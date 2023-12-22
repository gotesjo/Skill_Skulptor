using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SkillSkulptor.Models;

namespace SkillSkulptor.Controllers
{
    public class MessageController : Controller
    {
        private SsDbContext _dbContext;
		private UserManager<AppUser> userManager;
		private SignInManager<AppUser> signInManager;

		public MessageController(SsDbContext _db,SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager)
        {
            _dbContext = _db;
			userManager = _userManager;
			signInManager = _signInManager;
	}


		public IActionResult Index()
		{
			AppUser _loggedInUser = GetLoggedInUser();

			List<AppUser> _users = _dbContext.Users.ToList();

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

			List<AppUser> _users = _dbContext.Users.Where(user => user.Firstname.Contains(searchString)).ToList();

			var viewModel = new SearchViewModel
			{
				LoggedInUser = _loggedInUser,
				Users = _users,
				SearchString = searchString
			};

			return View("Index", viewModel);
		}

		[HttpGet]
		public IActionResult GetConversation(string userId)
		{
			// Hämta konversationsdata baserat på användarens ID
			string myUserID = GetLoggedInUser().Id;
			List<Message> conversation = _dbContext.Messages.Where(received => received.FromUserID == userId && received.ToUserID == myUserID).ToList();
			conversation.AddRange(_dbContext.Messages.Where(sent => sent.ToUserID == userId && sent.FromUserID == myUserID).ToList());

			conversation = conversation.OrderBy(message => message.MessageId).ToList();
			MessageServiceModel data = new MessageServiceModel();
			data.messagesObject = conversation;
			data.receiver = _dbContext.Users.Find(userId);

			return PartialView("_ConversationPartial", data);
		}



		private AppUser GetLoggedInUser()
		{
			AppUser loggedInUser = userManager.GetUserAsync(User).Result;
			return loggedInUser;
			
		}
	}
}
