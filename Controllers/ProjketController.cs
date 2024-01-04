using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
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
		private UserManager<AppUser> _userManager;


		public ProjektController(ILogger<ProjektController> logger, SsDbContext _db, UserManager<AppUser> userManager)
		{
			_Logger = logger;
			_dbContext = _db;
			_userManager = userManager;
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
                    AppUser loggedInUser = GetCurrentUserIdName();

                    projectObject.CreatedByUser = loggedInUser;

                    projectObject.MaxPeople++;

                    _dbContext.Projects.Add(projectObject);
                    _dbContext.SaveChanges();


					

                    var member = new ProjectMembers
                    {
                        ProjectId = projectObject.ProjectId,
                        UserId = loggedInUser.Id
                    };

                    _dbContext.ProjectMembers.Add(member);
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

		public IActionResult JoinProject(int ProjectId)
		{
			AppUser user = GetCurrentUserIdName();
            var project = _dbContext.Projects.Find(ProjectId);

            bool isAlreadyMember = _dbContext.ProjectMembers
				.Any(pm => pm.ProjectId == ProjectId && pm.UserId.Equals(user.Id));

            if (!isAlreadyMember)
			{
				if (project != null && project.MaxPeople <= 5)
				{
					project.MaxPeople++;

					_dbContext.SaveChanges();

                    var member = new ProjectMembers
                    {
                        ProjectId = ProjectId,
                        UserId = user.Id
                    };

					

                    _dbContext.ProjectMembers.Add(member);
                    _dbContext.SaveChanges();

                    return View("IndexProjekt");
				}
				else
				{
					ViewBag.Error = "Får max finnas 5 deltagare per projekt";
					return View("ProjketController");
				}
			}
			else
			{
				ViewBag.Error = "Du är redan del av detta projekt!";
                return View("IndexProjekt");
            }
			
		}

		public AppUser GetCurrentUserIdName()
		{
            AppUser loggedInUser = _userManager.GetUserAsync(User).Result;
            return loggedInUser;
        }

    }

}
