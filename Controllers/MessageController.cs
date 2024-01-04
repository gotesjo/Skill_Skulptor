using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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

			List<AppUser> _users = _dbContext.Users.ToList();

			var viewModel = new SearchViewModel
			{
				LoggedInUser = _loggedInUser,
				Users = _users,
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

			List<AppUser> _users = _dbContext.Users
				.Where(user => user.Firstname.Contains(searchString) || user.Lastname.Contains(searchString)).ToList();

			List<Education> _education = _dbContext.Educations
				.Where(education => education.Course.Contains(searchString) || education.Degree.Contains(searchString)).ToList();
			var viewModel = new SearchViewModel
			{
				LoggedInUser = _loggedInUser,
				Users = _users,
				Educations = _education,
				SearchString = searchString
			};

			return View("Index", viewModel);
		}

		[HttpGet]
		//public IActionResult GetConversation(int userId)
		//{
		//	// Hämta konversationsdata baserat på användarens ID
		//	int myUserID = 1;
		//	List<Message> conversation = _dbContext.Messages.Where(received => received.FromUserID == userId && received.ToUserID == myUserID).ToList();
		//	conversation.AddRange(_dbContext.Messages.Where(sent => sent.ToUserID == userId && sent.FromUserID == myUserID).ToList());

		//	conversation = conversation.OrderBy(message => message.MessageId).ToList();
		//	MessageServiceModel data = new MessageServiceModel();
		//	data.messagesObject = conversation;
		//	data.receiver = _dbContext.AppUsers.Find(userId);

		//	return PartialView("_ConversationPartial", data);
		//}



		private AppUser GetLoggedInUser()
		{
			return _dbContext.Users.First();
		}
	}
}
