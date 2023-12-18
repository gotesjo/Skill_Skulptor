using Microsoft.AspNetCore.Mvc;
using SkillSkulptor.Models;

namespace SkillSkulptor.Controllers
{
    public class ResumeController : Controller 
        
    {
        private readonly ILogger<ResumeController> _logger;
        private SsDbContext _dbContext;

        public ResumeController(ILogger<ResumeController> logger, SsDbContext _db)
        {
            _dbContext = _db;
            _logger = logger;
        }
        public IActionResult Index()
        {
            AppUser myUser = _dbContext.AppUsers.First();
          
            ViewBag.defaultPicturePath = "~/datafiles/pictures/defaultpicture.png";
            return View(myUser);
        }
    
    }
}
