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
        public async Task<IActionResult> Index()
        {
            var loggedInUser = await userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userCV = _dbContext.CVs.FirstOrDefault(cv => cv.BelongsTo == loggedInUser.Id);

            var viewModel = new ResumeViewModel
            {
                User = loggedInUser,
                UserCV = userCV
            };

            return View(viewModel);
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

