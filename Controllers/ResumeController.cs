using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkillSkulptor.Models;

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
            var viewModel = new ResumeViewModel
            {
                UserCV = new CV(),
                Experiences = new List<Experience>(),  // Lista för nya erfarenheter
                Educations = new List<Education>(),   // Lista för nya utbildningar
                Qualifications = new List<Qualification>() // Lista för nya kvalifikationer
            };

            return View(viewModel);
        }



        [HttpPost]
        public async Task<IActionResult> CreateCV(ResumeViewModel resumeViewModel)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await userManager.GetUserAsync(User);
                if (loggedInUser != null)
                {
                    var cv = new CV
                    {
                        BelongsTo = loggedInUser.Id,
                        Summary = resumeViewModel.UserCV.Summary,
                        PersonalLetter = resumeViewModel.UserCV.PersonalLetter,
                      
                        Experiences = new List<Experience>(resumeViewModel.Experiences),
                        Educations = new List<Education>(resumeViewModel.Educations),
                        Qualifications = new List<Qualification>(resumeViewModel.Qualifications)
                    };

                    _dbContext.CVs.Add(cv);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index"); 
                }
                else
                {
                    ModelState.AddModelError("", "Ingen inloggad användare hittades.");
                }
            }
            return View(resumeViewModel); // Om valideringsfel finns, visa formuläret igen
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

