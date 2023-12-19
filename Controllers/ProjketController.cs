using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillSkulptor.Models;

namespace SkillSkulptor.Controllers
{
	public class ProjektController : Controller
	{
		private readonly ILogger<ProjektController> _Logger;
		private SsDbContext _dbContext;


		public ProjektController(ILogger<ProjektController> logger, SsDbContext _db)
		{
			_Logger = logger;
			_dbContext = _db;
		}


		[HttpGet]
		public IActionResult IndexProjekt()
		{
            var _projects = _dbContext.Projects.ToList();
            ViewBag.Meddelande = "Totalt antal projekt: " + _projects.Count;
            return View(_projects);
		}

		[HttpGet]
		public IActionResult Index2()
		{
			return View();
		}

		//[HttpPost]
		//public IActionResult AddProjekt()
		//{
		//	Console.WriteLine("Denna borde inte kallas på addprojekt utan parametrear");
		//	return View(new Project());
		//}

		[HttpPost]

		public IActionResult AddProjekt(Project projectObject) 
		{
			try
			{
				if (ModelState.IsValid)
				{
                    _dbContext.Projects.Add(projectObject);
                    _dbContext.SaveChanges();
                    return RedirectToAction("IndexProjekt", projectObject);
                }
				else
				{
                    return View("index2", projectObject);
                }
				
					
				
			}
            catch (DbUpdateException ex)
            {

                _Logger.LogError(ex, "Ett fel inträffade vid försök att spara ändringar i databasen.");


                ViewBag.ErrorMessage = "Ett fel inträffade vid försök att spara ändringar i databasen.";


                return View("IndexProjekt", projectObject);
            }
        }

	}

}
