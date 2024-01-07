using Castle.Core.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using SkillSkulptor.Models;
using System.Diagnostics.Eventing.Reader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        // En metod som hämtar alla projekt och lägger det till en lista.
        // Skapar en Lista av ProjectViewModel som returnerar viewen för den ViewModelen. 
		[HttpGet]
		public IActionResult IndexProjekt()
		{
            
            List<Project> projects = _dbContext.Projects.Include(p => p.CreatedByUser).ToList();
            
            List<ProjectViewModel> projectViewModels = MapToViewModel(projects);
            ViewBag.Meddelande = "Totalt antal projekt: " + projectViewModels.Count;

            ViewBag.LoggedIn = GetCurrentUserIdName()?.Id;
            return View(projectViewModels);
		}

        //Tilldelar Viemodelen värdet av project model för en lista
        private List<ProjectViewModel> MapToViewModel(List<Project> projects)
        {
            List<ProjectViewModel> projectViewModels = new List<ProjectViewModel>();

            foreach (var project in projects)
            {
                ProjectViewModel viewModel = new ProjectViewModel
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    Description = project.Description,
                    Startdate = project.Startdate,
                    Enddate = project.Enddate,
                    MaxPeople = project.MaxPeople,
                    CreatedBy = project.CreatedBy,
                    CreatedByUser = project.CreatedByUser,
                    ProjectMembers = project.listProjectmembers.ToList(),
                    PersonCount = project.listProjectmembers.Count(),
                    IsUserPartOfProject = UserIsAlreadyPartOfProject(project)
                };


                projectViewModels.Add(viewModel);
            }

            return projectViewModels;
        }

        //För ett enskilt projekt
        private ProjectViewModel MapToViewModelSingel(Project projects)
        {

            ProjectViewModel viewModel = new ProjectViewModel
            {
                ProjectId = projects.ProjectId,
                ProjectName = projects.ProjectName,
                Description = projects.Description,
                Startdate = projects.Startdate,
                Enddate = projects.Enddate,
                MaxPeople = projects.MaxPeople,
                CreatedBy = projects.CreatedBy,
                CreatedByUser = projects.CreatedByUser,
                ProjectMembers = projects.listProjectmembers.ToList(),
                PersonCount = projects.listProjectmembers.Count(pm => pm.ProjectId == projects.ProjectId),
            };


            return viewModel;
        }

        public bool UserIsAlreadyPartOfProject(Project project)
        {
            return project.listProjectmembers.Any(pm => pm.UserId == GetCurrentUserId());
        }

        [HttpGet]
		public IActionResult Index2()
		{
			return View();
		}

        public IActionResult IndexShowProject(int projectId) 
        {

            var project = _dbContext.Projects.Find(projectId);

            List<ProjectViewModel> pro = new List<ProjectViewModel>();

            ViewBag.ProjectList = pro;

            if (project != null)
            {
                var model = MapToViewModelSingel(project);
                return View(model);
            }
            else 
            {
                NotFound();
            }

            return View();
            
        }

		[HttpGet]
		public IActionResult IndexEdit(int ProjectId) 
		{
            ViewBag.Count = _dbContext.ProjectMembers.Count(pm => pm.ProjectId == ProjectId);

                var project = _dbContext.Projects.Find(ProjectId);
                ProjectViewModel model = new ProjectViewModel();
                model.ProjectId = project.ProjectId;
                model.ProjectName = project.ProjectName;
                model.Description = project.Description;
                model.Startdate = project.Startdate;
                model.Enddate = project.Enddate;
                model.MaxPeople = project.MaxPeople;
                model.CreatedBy = project.CreatedBy;

                if (project == null)
                {

                    return RedirectToAction("IndexProjekt");
                }

                return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProject(ProjectViewModel updatedProject)
        {
            try
            {
                var existingProject = _dbContext.Projects
                    .Include(p => p.CreatedByUser)
                    .FirstOrDefault(p => p.ProjectId == updatedProject.ProjectId);


                    if (ModelState.IsValid)
                    {
                        existingProject.ProjectName = updatedProject.ProjectName;
                        existingProject.Description = updatedProject.Description;
                        existingProject.Startdate = updatedProject.Startdate;
                        existingProject.Enddate = updatedProject.Enddate;
                        existingProject.MaxPeople = updatedProject.MaxPeople;

                        _dbContext.Attach(existingProject.CreatedByUser);

                        _dbContext.Entry(existingProject).State = EntityState.Modified;

                        _dbContext.SaveChanges();

                        return RedirectToAction("IndexProjekt");
                    }
                    else
                    {
                        return View("IndexEdit");
                    }
                
            }
            catch (Exception ex)
            {

                _Logger.LogError(ex, "An error occurred while updating the project.");


                return View("Error");
            }
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

			if (user != null && project != null)
			{
				bool isAlreadyMember = _dbContext.ProjectMembers
					.Any(pm => pm.ProjectId == ProjectId && pm.UserId.Equals(user.Id));

                TempData["isAlreadyMember"] = isAlreadyMember;

                if (!isAlreadyMember)
				{
                        int personCount = _dbContext.ProjectMembers.Count(pm => pm.ProjectId == ProjectId);
                        

                    if (personCount < project.MaxPeople)
                        {
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
                            ViewBag.Error = "Får max finnas " + project.MaxPeople + " deltagare per projekt";
                            return RedirectToAction("IndexProjekt");
                        }
                    }
                else
                {
                    ViewBag.Error = "Du är redan medlem";
                    return RedirectToAction("IndexProjekt");
                }
				}
            else
            { 
                ViewBag.Error = "Finns inge projekt";
                return View("IndexProjekt");
            }
		}

		public AppUser GetCurrentUserIdName()
		{
            
                AppUser loggedInUser = _userManager.GetUserAsync(User).Result;
                return loggedInUser;
        }

        public string GetCurrentUserId()
        {
            AppUser loggedInUser = _userManager.GetUserAsync(User).Result;
            return loggedInUser?.Id;
        }

    }

}
