using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSkulptor.Models;

namespace SkillSkulptor.Controllers
{
	public class ProjketController : Controller
	{
		private SsDbContext _dbContext;


		public ProjketController(SsDbContext _db)
		{
			_dbContext = _db;
		}


		[HttpGet]
		public IActionResult Projekt()
		{
			List<Project> _projects = new List<Project>();
			return View(_projects);
		}
	}

}
