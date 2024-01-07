using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSkulptor.Models;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

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

        // Denna metod tar alla projekt som skapats och visar de 3 senaste p� hemsk�rmen brevid alla CVs.  
        // Metoden tar i �tanke om anv�ndaren �r inloggad eller ej, och h�mtar objekt av projekt beroende 
        // p� olika krav baserat p� en status f�r inloggning. 
        // De krav som kollas �r om anv�ndaren som skapat projektet �r aktiv och om den har sin profil g�md. 
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
                var currentID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                List<CV> allCv = _dbContext.CVs
                    .Where(cv => cv.fkUser.Id != currentID)
                    .OrderByDescending(cv => cv.CVID)
                    .Take(3).Where(cv => cv.fkUser.Active == true)
                    .ToList();
                List<CV> testCV = new List<CV>();
                ViewBag.Heading = "Senaste cv p� sidan";
                return View(allCv);
            }
            else
            {
                List<CV> vissaCV = _dbContext.CVs
                    .OrderByDescending(cv => cv.CVID)
                    .Take(3)
                    .Where(cv => cv.fkUser.ProfileAccess == true && cv.fkUser.Active == true)
                    .ToList();
                ViewBag.Heading = "Senaste cv p� sidan";
                return View(vissaCV);
            }

        }

        public SsDbContext Get_dbContext()
        {
            return _dbContext;
        }

        // Metodens funktionalitet �r att s�ka efter CV baserat p� ett f�rnamn, ett efternamn,
        // eller en kombination av de b�da, och sedan visa det CV som sen g�r att se vidare p�.
        // Denna metod tar �ven i �tanke om anv�ndaren som s�ker �r inloggad eller ej, 
        // och �ven om anv�ndaren som det s�ks p� �r aktiv eller ej.
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
            // fethcar alla CV fr�n databasen till en lista. 
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
                        .Where(a => a.fkUser.Active == true &&
                        a.fkUser.Firstname.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                        (a.fkUser.Lastname.Contains(search, StringComparison.OrdinalIgnoreCase))).ToList();
                        ViewBag.Heading = "CV med namnet " + search;
                        // StringComparison.OrdinalIgnoreCase anv�nds under alla s�kningar f�r �tt s�kerst�lla att s�kstr�ngen fungerar oavsett versaler.
                    }
                    // annars kommer denna else sats att �ven filtera p� om kolumnen ProfileAccess �r sann
                    // eller falsk, och endast ta med objekt d�r det �r falskt. 
                    else
                    {
                        CvSearched = CvSearched
                        .Where(a => a.fkUser.ProfileAccess == true && a.fkUser.Active == true && 
                        a.fkUser.Firstname.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                        (a.fkUser.Lastname.Contains(search, StringComparison.OrdinalIgnoreCase)))
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
                    .Where(a => a.fkUser.ProfileAccess == true && a.fkUser.Active == true &&
                    a.fkUser.Firstname.Contains(firstName, StringComparison.OrdinalIgnoreCase) && 
                    a.fkUser.Lastname.Contains(lastName, StringComparison.OrdinalIgnoreCase)).ToList();
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