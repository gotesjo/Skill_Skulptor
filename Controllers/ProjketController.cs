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
            var projects = _dbContext.Projects.ToList();
            ViewBag.Meddelande = "Totalt antal projekt: " + projects.Count;

			ViewBag.LoggedIn = GetCurrentUserIdName()?.Id;

            return View(projects);
		}

		[HttpGet]
		public IActionResult Index2()
		{
			return View();
		}


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

		public IActionResult RemoveProject(int projectId) 
		{
            AppUser user = GetCurrentUserIdName();
            var project = _dbContext.Projects.FirstOrDefault(p => p.ProjectId == projectId);

            if (user != null && project != null && project.CreatedByUser.Firstname == project.CreatedByUser.Firstname)
            {
                var projectMembers = _dbContext.ProjectMembers.Where(pm => pm.ProjectId == projectId);
                _dbContext.ProjectMembers.RemoveRange(projectMembers);
                
                _dbContext.Projects.Remove(project);
                _dbContext.SaveChanges();

                ViewBag.SuccessMessage = "Projektet har tagits bort!";
            }
            else
            {
                ViewBag.ErrorMessage = "Du har inte behörighet att ta bort detta projekt eller så finns projektet inte.";
            }

            return RedirectToAction("IndexProjekt");
        
    }

		public IActionResult JoinProject(int ProjectId)
		{
			AppUser user = GetCurrentUserIdName();
            var project = _dbContext.Projects.Find(ProjectId);

			if (user != null)
			{
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

						return RedirectToAction("IndexProjekt");
					}
					else
					{
						ViewBag.Error = "Får max finnas 5 deltagare per projekt";
						return RedirectToAction("ProjketController");
					}
				}
				else
				{
                    TempData["ErrorMessage"] = "Du är redan del av detta projekt!";
                    return RedirectToAction("IndexProjekt");
                    
                }
			}
			else
			{
				return RedirectToAction("IndexProjekt");
			}
			
		}
		public AppUser GetCurrentUserIdName()
		{
            AppUser loggedInUser = _userManager.GetUserAsync(User).Result;
            return loggedInUser;
        }

    }

}
