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
            try
            {
                Project _project = _dbContext.Projects.OrderByDescending(p => p.ProjectId).First();
                ViewBag.Project = _project;
            }
            catch
            {
                Project ExP = new Project();
                ExP.ProjectName = "Finns inga projekt";
                ViewBag.Project = ExP;

            }

            List<CV> allCv= _dbContext.CVs.OrderByDescending(cv => cv.CVID).Take(3).ToList();
            return View(allCv);
        }


        [HttpPost]
        public IActionResult Index(string search)
        {
            try
            {
                Project _project = _dbContext.Projects.OrderByDescending(p => p.ProjectId).First();
                ViewBag.Project = _project;
            }
            catch
            {
                Project ExP = new Project();
                ExP.ProjectName = "Finns inga projekt";
                ViewBag.Project = ExP;
            }

            string[] searchTerms = search.Split(' ');

            if (searchTerms.Length == 1)
            {
                List<CV> CvSearched = _dbContext.CVs
                    .Where(a => a.fkUser.Firstname.Contains(search) || a.fkUser.Lastname.Contains(search))
                    .ToList();

                return View(CvSearched);
            }
            else if (searchTerms.Length == 2)
            {
                string firstName = searchTerms[0];
                string lastName = searchTerms[1];

                List<CV> CvSearched = _dbContext.CVs
                    .Where(a => a.fkUser.Firstname.Contains(firstName) && a.fkUser.Lastname.Contains(lastName))
                    .ToList();

                return View(CvSearched);
            }
            else
            {
                return View(new List<CV>());
            }
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