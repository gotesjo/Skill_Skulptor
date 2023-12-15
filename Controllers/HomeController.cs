using Microsoft.AspNetCore.Mvc;
using SkillSkulptor.Models;
using System.Diagnostics;

namespace SkillSkulptor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SsDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, SsDbContext _db)
        {
            _dbContext = _db;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<CV> allCv= _dbContext.CVs.ToList();
            return View(allCv);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}