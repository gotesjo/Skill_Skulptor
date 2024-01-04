using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            

            if(choosenUser != null)
            {
                viewModel.UserCV = userCV;
                viewModel.User = choosenUser;
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult CreateCV()
        {
            CreateResumeModel model = new CreateResumeModel();
            CV cV = new CV();

            
            cV.Experiences = new List<Experience>();
            cV.Educations = new List<Education>();
            cV.Qualifications = new List<Qualification>();

            Experience experience = new Experience();
            cV.Experiences.Add(experience);

            Education education = new Education();
            cV.Educations.Add(education);

            Qualification quali = new Qualification();
            cV.Qualifications.Add(quali);

            model.UserCV = cV;

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> CreateCV(CreateResumeModel model)
        {
            AppUser loggedInUser = await userManager.GetUserAsync(User);
            model.UserCV.fkUser = loggedInUser;

            ModelState.Clear();

            if (ModelState.IsValid)
            {

                if (loggedInUser != null)
                {
                    model.UserCV.BelongsTo = loggedInUser.Id;

                    _dbContext.CVs.Add(model.UserCV);
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

