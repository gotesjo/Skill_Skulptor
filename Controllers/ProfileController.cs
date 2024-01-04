using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSkulptor.Models;
using System.Linq;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SkillSkulptor.Controllers
{
	public class ProfileController : Controller
	{
        private SsDbContext _dbContext;
		private UserManager<AppUser> userManager;
		private IWebHostEnvironment _hostingEnvironment;

        public ProfileController(SsDbContext db, IWebHostEnvironment hostingEnvironment, UserManager<AppUser> _userManager)
        {
            _dbContext = db;
			userManager = _userManager;
			_hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
			AppUser user = userManager.GetUserAsync(User).Result;

			return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
			AppUser user = userManager.GetUserAsync(User).Result;

			return View(user);
		}

        [HttpGet]
        public IActionResult EditCV()
        {
            AppUser user = userManager.GetUserAsync(User).Result;

            // Skapa en ResumeViewModel-instans
            var resumeViewModel = new ResumeViewModel
            {
                // Antag att ResumeViewModel har en egenskap för att lagra användarinformation
                User = user,
                UserCV = user.userCV // Eller hur du nu hämtar användarens CV
            };

            return View(resumeViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> ProfileEdit(AppUser user, IFormFile file)
        {
            try
            {
                // Hämta den befintliga användaren från databasen
                var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);

                if (existingUser == null)
                {
                    // Hantera fallet då användaren inte hittades
                    return NotFound(); // eller någon annan lämplig åtgärd
                }

                // Uppdatera övriga egenskaper (förnamn, efternamn, e-post etc.)
                existingUser.Firstname = user.Firstname;
                existingUser.Lastname = user.Lastname;
                existingUser.Email = user.Email;
                existingUser.Phonenr = user.Phonenr;
                existingUser.Active = user.Active;
                existingUser.ProfileAccess = user.ProfileAccess;

                if (user.fkAddress != null)
                {
                    if (existingUser.fkAddress == null)
                    {
                        existingUser.fkAddress = new Adress
                        {
                            Street = user.fkAddress?.Street ?? "",
                            City = user.fkAddress?.City ?? "",
                            ZipCode = user.fkAddress?.ZipCode ?? "",
                            Country = user.fkAddress?.Country ?? ""
                        };
                    }



                    _dbContext.Adresses.Add(existingUser.fkAddress); // Lägg till den nya adressen i databasen
                }
                else
                {
                    existingUser.fkAddress.Street = user.fkAddress.Street;
                    existingUser.fkAddress.City = user.fkAddress.City;
                    existingUser.fkAddress.ZipCode = user.fkAddress.ZipCode;
                    existingUser.fkAddress.Country = user.fkAddress.Country;
                
                }

                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        if (existingUser.fkPicture == null)
                        {
                            existingUser.fkPicture = new Profilepicture(); // Ersätt 'Profilepicture' med korrekt klassnamn
                        }

                        existingUser.fkPicture.ImageData = stream.ToArray();
                        existingUser.fkPicture.Filename = file.FileName;
                    }
                }

                _dbContext.Update(existingUser);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Error");
            }
        }


        public async Task<IActionResult> UserImage(string userId)
        {
            var userPicture = await _dbContext.ProfilePictures
                                              .FirstOrDefaultAsync(p => p.pictureUser.Id == userId);

            if (userPicture?.ImageData != null)
            {
                // Typen JPEG, uppdatera MIME-typen enligt bildformatet
                return File(userPicture.ImageData, "image/jpeg");
            }


            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "datafiles", "pictures", "default-profile.jpg");
            var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            // Ange rätt MIME-typ beroende på filtypen
            var mimeType = "image/jpeg"; // Om din bild är en JPEG. Anpassa efter din bildtyp.

            return File(imageBytes, mimeType);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCV(ResumeViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingUser = _dbContext.Users
                                         .Include(u => u.userCV)
                                             .ThenInclude(cv => cv.Experiences)
                                         .Include(u => u.userCV)
                                             .ThenInclude(cv => cv.Educations)
                                         .Include(u => u.userCV)
                                             .ThenInclude(cv => cv.Qualifications)
                                         .FirstOrDefault(u => u.Id == userId);

            if (existingUser == null)
            {
                return NotFound();
            }

            // Uppdatera CV-information
            var userCV = existingUser.userCV ?? new CV();
            userCV.Summary = model.UserCV.Summary;
            userCV.PersonalLetter = model.UserCV.PersonalLetter;

            if (model.UserCV.Experiences != null)
            {
                foreach (var experience in _dbContext.Experiences)
                {
                    var existingExperience = userCV.Experiences.FirstOrDefault(e => e.ExId == experience.ExId);
                    if (existingExperience != null)
                    {
                        // Uppdatera befintlig erfarenhet med nya värden
                        existingExperience.Position = experience.Position;
                        existingExperience.Employer = experience.Employer;
                        existingExperience.Description = experience.Description;
                        existingExperience.StartDate = experience.StartDate;
                        existingExperience.EndDate = experience.EndDate;

                        _dbContext.Experiences.Update(existingExperience);
                    }
                }
            }



            // Uppdatera utbildningar
            UpdateEducations(userCV, model.Educations);

            // Uppdatera kvalifikationer
            UpdateQualifications(userCV, model.Qualifications);

            _dbContext.Update(existingUser);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("EditCV");
        }

        private async Task UpdateExperiences(CV userCV, List<Experience> experiences)
        {
            foreach (var experience in experiences)
            {
                var existingExperience = userCV.Experiences.FirstOrDefault(e => e.ExId == experience.ExId);
                if (existingExperience != null)
                {
                    // Uppdatera befintlig erfarenhet med nya värden
                    existingExperience.Position = experience.Position;
                    existingExperience.Employer = experience.Employer;
                    existingExperience.Description = experience.Description;
                    existingExperience.StartDate = experience.StartDate;
                    existingExperience.EndDate = experience.EndDate;

                    _dbContext.Experiences.Update(existingExperience);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }


        private void UpdateEducations(CV userCV, List<Education> educations)
        {
            foreach (var education in educations)
            {
                var existingEducation = userCV.Educations.FirstOrDefault(e => e.EdID == education.EdID);
                _dbContext.Entry(existingEducation).CurrentValues.SetValues(education);
            }
        }

        private void UpdateQualifications(CV userCV, List<Qualification> qualifications)
        {
            foreach (var qualification in qualifications)
            {
                var existingQualification = userCV.Qualifications.FirstOrDefault(q => q.QID == qualification.QID);
                _dbContext.Entry(existingQualification).CurrentValues.SetValues(qualification);
            }
        }





    }
}

