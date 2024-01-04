using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSkulptor.Models;
using System.Linq.Expressions;

namespace SkillSkulptor.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ILogger<ResumeController> _logger;
        private SsDbContext _dbContext;
        private UserManager<AppUser> userManager;

        public ResumeController(ILogger<ResumeController> logger, SsDbContext db, UserManager<AppUser> userManager)
        {
            _dbContext = db;
            _logger = logger;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? id)
        {
            string _id = null;
            ResumeViewModel viewModel = new ResumeViewModel();

            if (id == null)
            {
                AppUser loggedInUser = await userManager.GetUserAsync(User);
                _id = loggedInUser.Id;
            }
            else
            {
                _id = id;
            }
            AppUser choosenUser;
            CV userCV;

            try
            {
                choosenUser = _dbContext.Users.Find(_id);
                userCV = choosenUser.userCV;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }


            if (choosenUser != null)
            {
                viewModel.UserCV = userCV;
                viewModel.User = choosenUser;
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpGet]
        public IActionResult CreateCV()
        {
            CreateResumeModel model = new CreateResumeModel();

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCV(CreateResumeModel model)
        {
            AppUser loggedInUser = await userManager.GetUserAsync(User);


            if (ModelState.IsValid)
            {

                if (loggedInUser != null)
                {
                    CV _cv = new CV();
                    _cv.fkUser = loggedInUser;
                    _cv.Summary = model.Summary;
                    _cv.PersonalLetter = model.PersonalLetter;
                    _cv.Clicks = 0;

                    //Education
                    if(model.Education != null)
                    {
                        _cv.Educations.Add(MakeEducation(model.Education));
                    }
                    else
                    {
                        ModelState.Remove("model.Education");
                    }
                    //Experience
                    if (model.Experience != null)
                    {
                        _cv.Experiences.Add(MakeExperience(model.Experience));
                    }
                    else
                    {
                        ModelState.Remove("model.Experience");
                    }
                    //Qualification
                    if (model.Qualification != null)
                    {
                        _cv.Qualifications.Add(MakeQualification(model.Qualification));
                    }
                    else
                    {
                        ModelState.Remove("model.Qualification");
                    }

                    _dbContext.CVs.Add(_cv);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Ingen inloggad användare hittades.");
                }
            }

            return View(model); // Om valideringsfel finns, visa formuläret igen
        }

        private Education MakeEducation(CreateEducationModel model)
        {
            Education ed = new Education();
            ed.StartDate = model.EdStartDate;
            ed.EndDate = model.EdEndDate;
            ed.Institution = model.Institution;
            ed.Course = model.Course;
            ed.Degree = model.Degree;

            return ed;
        }
        private Experience MakeExperience(CreateExperienceModel model)
        {
            Experience ex = new Experience();
            ex.StartDate = model.ExStartDate;
            ex.EndDate = model.ExEndDate;
            ex.Position = model.Position;
            ex.Description = model.ExDescription;
            ex.Employer = model.Employer;

            return ex;
        }
        private Qualification MakeQualification(CreateQualificationModel model)
        {
            Qualification qu = new Qualification();
            qu.QName = model.QName;
            qu.Description = model.QDescription;

            return qu;
        }





        [HttpGet]
        public async Task<IActionResult> DeleteCV()
        {
            var loggedInUser = await userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                // Användaren är inte inloggad
                return RedirectToAction("Login", "Account"); // Eller någon annan lämplig åtgärd
            }

            var userCV = _dbContext.CVs.FirstOrDefault(cv => cv.BelongsTo == loggedInUser.Id);
            if (userCV == null)
            {
                // Användaren har inget CV att ta bort eller försöker ta bort någon annans CV
                TempData["ErrorMessage"] = "Du har inte behörighet att ta bort detta CV.";
                return RedirectToAction("Index", "Home"); 
            }

            _dbContext.CVs.Remove(userCV);
            await _dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Ditt CV har tagits bort.";
            return RedirectToAction("Index", "Home"); 
        }


    }
}




//[HttpGet("{id:int}")]
//public IActionResult Index(int id)
//{
//  var myUser = _dbContext.Users.FirstOrDefault(u => u.UserId == id);
//        return View (myUser);
//}
//public async Task<IActionResult> UserImage(int userId)
//{
//    var userPicture = await _dbContext.ProfilePictures
//                                      .FirstOrDefaultAsync(p => p.pictureUser.UserId == userId);

//    if (userPicture?.ImageData != null)
//    {
//        // Anta att bilden är av typen JPEG, uppdatera MIME-typen enligt bildformatet
//        return File(userPicture.ImageData, "image/jpeg");
//    }


//    var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "default-profile.png");
//    var imageBytes = await System.IO.File.ReadAllBytesAsync(path);
//    return File(imageBytes, "image/png"); // Anpassa MIME-typen enligt standardbilden
//}

