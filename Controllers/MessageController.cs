using Microsoft.AspNetCore.Mvc;
using SkillSkulptor.Models;

namespace SkillSkulptor.Controllers
{
    public class MessageController : Controller
    {
        private SsDbContext _dbContext;

        public MessageController(SsDbContext _db)
        {
            _dbContext = _db;
        }


		public IActionResult Index()
		{
			AppUser _loggedInUser = GetLoggedInUser(); 

			List<AppUser> _users = _dbContext.AppUsers.ToList();

			var viewModel = new SearchViewModel
			{
				LoggedInUser = _loggedInUser,
				Users = _users
			};

			return View(viewModel);
		}

		[HttpPost]
		public IActionResult Search(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				return RedirectToAction("Index");
			}

			AppUser _loggedInUser = GetLoggedInUser(); 

			List<AppUser> _users = _dbContext.AppUsers.Where(user => user.Firstname.Contains(searchString)).ToList();

			var viewModel = new SearchViewModel
			{
				LoggedInUser = _loggedInUser,
				Users = _users,
				SearchString = searchString
			};

			return View("Index", viewModel);
		}

		private AppUser GetLoggedInUser()
		{
			return _dbContext.AppUsers.First();
		}
	}
}
