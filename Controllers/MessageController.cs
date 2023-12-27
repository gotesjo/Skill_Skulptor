using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SkillSkulptor.Models;

namespace SkillSkulptor.Controllers
{
	[Authorize]
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
			_users.Remove(_loggedInUser);

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
		public IActionResult GetConversation(string otherUserID)
		{
			// Hämta konversationsdata baserat på användarens ID
			string currentUserId = GetLoggedInUser().Id;
			List<Message> conversation = _dbContext.Messages.Where(received => received.FromUserID == otherUserID && received.ToUserID == currentUserId).ToList();
			conversation.AddRange(_dbContext.Messages.Where(sent => sent.ToUserID == otherUserID && sent.FromUserID == currentUserId).ToList());

			conversation = conversation.OrderBy(message => message.MessageId).ToList();
			List<Message_object> list_objects = new List<Message_object>();

			foreach (Message message in conversation)
			{
				Message_object mess_obj = new Message_object();
				var messdir = "";
				var messName = "";

				if (message.FromUserID == otherUserID)
				{
					messdir = "chat-message-left";
					messName = message.fkFromUser.Firstname + " " + message.fkFromUser.Lastname;
				}
				else
				{
					messdir = "chat-message-right";
					messName = message.fkFromUser.Firstname + " " + message.fkFromUser.Lastname;
				}

				if (message.fkFromUser?.fkPicture?.ImageData is not null)
				{
					mess_obj.ImageId = message.fkFromUser.Id;
				}

				mess_obj.classCss = messdir;
				mess_obj.MessageName = messName;
				mess_obj.MessageId = message.MessageId;
				mess_obj.Text = message.Text;

				list_objects.Add(mess_obj);
			}

			MessageServiceModel data = new MessageServiceModel();
			data.messagesObjects = list_objects;
			data.receiver = _dbContext.Users.Find(otherUserID);

			return PartialView("_ConversationPartial", data);
		}

		[HttpPost]
		public IActionResult Send(string Newmessage, string receiverId)
		{
			Message message = new Message();
			message.Text = Newmessage;
			message.ToUserID = receiverId;
			message.Datum = DateTime.Now;
			message.FromUserID = userManager.GetUserAsync(User).Result.Id;
			message.ViewStatus = false;

			_dbContext.Add(message);
			_dbContext.SaveChanges();

			return RedirectToAction("GetConversation", new {otherUserId = message.ToUserID });
		}



		private AppUser GetLoggedInUser()
		{
			AppUser loggedInUser = userManager.GetUserAsync(User).Result;
			return loggedInUser;
			
		}
	}
}
