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

        // Denna metod tar alla projekt som skapats och visar de 3 senaste på hemskärmen brevid alla CVs.  
        // Metoden tar i åtanke om användaren är inloggad eller ej, och hämtar objekt av projekt beroende 
        // på olika krav baserat på en status för inloggning. 
        // De krav som kollas är om användaren som skapat projektet är aktiv och om den har sin profil gömd. 
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
                ViewBag.Heading = "Senaste cv på sidan";
                return View(allCv);
            }
            else
            {
                List<CV> vissaCV = _dbContext.CVs
                    .OrderByDescending(cv => cv.CVID)
                    .Take(3)
                    .Where(cv => cv.fkUser.ProfileAccess == true && cv.fkUser.Active == true)
                    .ToList();
                ViewBag.Heading = "Senaste cv på sidan";
                return View(vissaCV);
            }

        }

        public SsDbContext Get_dbContext()
        {
            return _dbContext;
        }

        // Metodens funktionalitet är att söka efter CV baserat på ett förnamn, ett efternamn,
        // eller en kombination av de båda, och sedan visa det CV som sen går att se vidare på.
        // Denna metod tar även i åtanke om användaren som söker är inloggad eller ej, 
        // och även om användaren som det söks på är aktiv eller ej.
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
            // fethcar alla CV från databasen till en lista. 
            List<CV> CvSearched = _dbContext.CVs.ToList();

           
            if (search != null)
            {
                string[] searchTerms = search.Split(' ');
                // Om användaren är inloggad I.E., auktoriserad filtrerar den endast på för- och eftenamn
                // för alla CVs. 
                if (searchTerms.Length == 1)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        CvSearched = CvSearched
                        .Where(a => a.fkUser.Active == true &&
                        a.fkUser.Firstname.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                        (a.fkUser.Lastname.Contains(search, StringComparison.OrdinalIgnoreCase))).ToList();
                        ViewBag.Heading = "CV med namnet " + search;
                        // StringComparison.OrdinalIgnoreCase används under alla sökningar för ätt säkerställa att söksträngen fungerar oavsett versaler.
                    }
                    // annars kommer denna else sats att även filtera på om kolumnen ProfileAccess är sann
                    // eller falsk, och endast ta med objekt där det är falskt. 
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
                // även om användaren skriver båda för- och efternamn ska metoden fungera, därav en extra sats
                // som splittar upp de båda strängarna. 
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
                // Om den inte hittar ett CV kopplat till någon användare skall ett meddelande komma fram
                // som låter användaren veta det. 
                {
                    if (CvSearched.Count == 0)
                    {
                        ViewBag.Heading = "Vi hittade inga CV utifrån din sökning";
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