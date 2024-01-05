using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
			AppUser user = userManager.GetUserAsync(User).Result;
            PasswordViewModel pwModel = new PasswordViewModel();

            return View(pwModel);
		}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordViewModel pwModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = userManager.GetUserAsync(User).Result;

                try
                {
                    var result = await userManager.ChangePasswordAsync(user, pwModel.OldPassword, pwModel.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Profile");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View(pwModel);

                }
            }
            else
            {
                return View(pwModel);
            }

            return View(pwModel);

        }


        [HttpGet]
        public IActionResult EditCV()
        {
            AppUser user = userManager.GetUserAsync(User).Result;

            var resumeViewModel = new ResumeViewModel
            {
                User = user,
                UserCV = user.userCV 
            };

            return View(resumeViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> ProfileEdit(AppUser user, IFormFile file)
        {
            try
            {
                // Hämta den befintliga användaren från databasen
                var _existingUser = await _dbContext.Users.FindAsync(user.Id);

                if (_existingUser == null)
                {
                    // Hantera fallet då användaren inte hittades
                    return View("Index", "Home");
                }

                // Uppdatera övriga egenskaper (förnamn, efternamn, e-post etc.)
                if(user.Firstname != null)
                {
                    _existingUser.Firstname = user.Firstname;
                }
                if (user.Firstname != null)
                {
                    _existingUser.Lastname = user.Lastname;
                }
                if (user.Firstname != null)
                {
                    _existingUser.Email = user.Email;
                }
                if (user.Firstname != null)
                {
                    _existingUser.Phonenr = user.Phonenr;
                }
                if (user.Firstname != null)
                {
                    _existingUser.Active = user.Active;
                }
                if (user.Firstname != null)
                {
                    _existingUser.ProfileAccess = user.ProfileAccess;
                }

                if(_existingUser.fkAddress == null && user.fkAddress != null)
                {
                    Adress newAddress = new Adress();
                    _existingUser.fkAddress = newAddress;
                    
                }

                //Adress
                
                    _existingUser.fkAddress.Street = user.fkAddress.Street;
                
                if (user.fkAddress.City != null)
                {
                    _existingUser.fkAddress.City = user.fkAddress.City;
                }
                if (user.fkAddress.ZipCode != null)
                {
                    _existingUser.fkAddress.ZipCode = user.fkAddress.ZipCode;
                }
                if (user.fkAddress.Country != null)
                {
                    _existingUser.fkAddress.Country = user.fkAddress.Country;
                }


                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        if (_existingUser.fkPicture == null)
                        {
                            _existingUser.fkPicture = new Profilepicture(); // Ersätt 'Profilepicture' med korrekt klassnamn
                        }

                        _existingUser.fkPicture.ImageData = stream.ToArray();
                        _existingUser.fkPicture.Filename = file.FileName;
                    }
                }

 
                var result = await _dbContext.SaveChangesAsync();
                if(result > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index", "Error");
            }
            return RedirectToAction("Index", "Error");
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

            
            var mimeType = "image/jpeg"; 

            return File(imageBytes, mimeType);

        }
    }
}

