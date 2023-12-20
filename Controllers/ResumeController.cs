using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSkulptor.Models;

namespace SkillSkulptor.Controllers
{
    public class ResumeController : Controller 
        
    {
        private readonly ILogger<ResumeController> _logger;
        private SsDbContext _dbContext;
        private IWebHostEnvironment _hostingEnvironment;

        public ResumeController(ILogger<ResumeController> logger, SsDbContext _db, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = _db;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            AppUser myUser = _dbContext.AppUsers.First();
          
            ViewBag.defaultPicturePath = "~/datafiles/pictures/defaultpicture.png";
            return View(myUser);
        }
        public async Task<IActionResult> UserImage(int userId)
        {
            var userPicture = await _dbContext.ProfilePictures
                                              .FirstOrDefaultAsync(p => p.pictureUser.UserId == userId);

            if (userPicture?.ImageData != null)
            {
                // Anta att bilden är av typen JPEG, uppdatera MIME-typen enligt bildformatet
                return File(userPicture.ImageData, "image/jpeg");
            }


            var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "default-profile.png");
            var imageBytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(imageBytes, "image/png"); // Anpassa MIME-typen enligt standardbilden
        }

    }
}
