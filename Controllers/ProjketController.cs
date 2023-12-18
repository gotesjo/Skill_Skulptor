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
		public IActionResult Index()
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
	}

}
