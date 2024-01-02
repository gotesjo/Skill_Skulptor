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
            // Visa sidan för att skapa ett nytt CV
            return View(new CV());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCV(CV cvModel)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await userManager.GetUserAsync(User);
                if (loggedInUser != null)
                {
                    cvModel.BelongsTo = loggedInUser.Id;
                    _dbContext.CVs.Add(cvModel);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index"); // Återgå till översiktssidan efter att CV:t har skapats
                }
                else
                {
                    ModelState.AddModelError("", "Ingen inloggad användare hittades.");
                }
            }
            return View(cvModel); // Om valideringsfel finns, visa formuläret igen
        }

        // Eventuella ytterligare metoder och logik...
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

