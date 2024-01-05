using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSkulptor.Models;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;

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
            catch (Exception ex)
            {
                Project ExP = new Project();
                ExP.ProjectName = "Finns inga projekt";
                Console.WriteLine(ex.Message);
                ViewBag.Project = ExP;

            }
            if (User.Identity.IsAuthenticated)
            {
                List<CV> allCv = _dbContext.CVs.OrderByDescending(cv => cv.CVID).Take(3).ToList();
                List<CV> testCV = new List<CV>();
                ViewBag.Heading = "Senaste cv p� sidan";
                return View(allCv);
            }
            else
            {
                List<CV> vissaCV = _dbContext.CVs.OrderByDescending(cv => cv.CVID).Take(3).Where(cv => cv.fkUser.ProfileAccess == false).ToList();
                ViewBag.Heading = "Senaste cv p� sidan";
                return View(vissaCV);
            }

        }

        public SsDbContext Get_dbContext()
        {
            return _dbContext;
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
            // fethcar alla CV fr�n databasen
            List<CV> CvSearched = _dbContext.CVs.ToList();

           
            if (search != null)
            {
                string[] searchTerms = search.Split(' ');
                // Om anv�ndaren �r inloggad I.E., auktoriserad filtrerar den endast p� f�r- och eftenamn
                // f�r alla CVs. 
                if (searchTerms.Length == 1)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        CvSearched = CvSearched
                        .Where
                        (a => a.fkUser.Firstname.Contains(search) ||
                        a.fkUser.Lastname.Contains(search)).ToList();
                        ViewBag.Heading = "CV med namnet " + search;
                    }
                    // annars kommer denna else sats att �ven filtera p� om kolumnen ProfileAccess �r sann
                    // eller falsk, och endast ta med objekt d�r det �r falskt. 
                    else
                    {
                        CvSearched = CvSearched
                        .Where
                        (a => a.fkUser.ProfileAccess == false &&
                        (a.fkUser.Firstname.Contains(search) || a.fkUser.Lastname.Contains(search)))
                        .ToList();
                        ViewBag.Heading = "CV med namnet " + search;

                    }
                }
                // �ven om anv�ndaren skriver b�da f�r- och efternamn ska metoden fungera, d�rav en extra sats
                // som splittar upp de b�da str�ngarna. 
                if (searchTerms.Length == 2)
                {
                    string firstName = searchTerms[0];
                    string lastName = searchTerms[1];

                    CvSearched = CvSearched
                    .Where(a => a.fkUser.ProfileAccess == false &&
                    a.fkUser.Firstname.Contains(firstName) && a.fkUser.Lastname.Contains(lastName)).ToList();
                    ViewBag.Heading = "CV med namnet " + search;

                } 
                else
                // Om den inte hittar ett CV kopplat till n�gon anv�ndare skall ett meddelande komma fram
                // som l�ter anv�ndaren veta det. 
                {
                    if (CvSearched.Count == 0)
                    {
                        ViewBag.Heading = "Vi hittade inga CV utifr�n din s�kning";
                    }
                    return View(CvSearched);
                }
            }
            // returnerar den filtrerade eller ofiltrerade listan till vyn. 
            return View(CvSearched);
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