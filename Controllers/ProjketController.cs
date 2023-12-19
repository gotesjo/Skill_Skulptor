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

			Console.WriteLine("Hej jag används fakrsikt: Addprojetk");

			if(projectObject == null)
			{
				return RedirectToAction("Index");
			}
			else
			{ 
				_dbContext.Projects.Add(projectObject);
				_dbContext.SaveChanges();
				return RedirectToAction("IndexProjekt");
            }


        }

	}

}
