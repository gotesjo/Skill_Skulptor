using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.VisualBasic;
using SkillSkulptor.Models;
using AutoMapper;

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

		[Authorize]
		public IActionResult Start()
		{
			AppUser _loggedInUser = GetLoggedInUser();

			//Lägger till alla anvvändare  man kan skicka till och tar bort sin egna profil
			List<AppUser> _users = _dbContext.Users.Where(u => u.Active).ToList();
			_users.Remove(_loggedInUser);
			//Lägger till Anonyma användare som har skickat via Meddelandefunktionen på CV sidan
			_users.AddRange(GetUnknonwUsers(_loggedInUser));

			var viewModel = new SearchViewModel
			{
				LoggedInUser = _loggedInUser,
				Users = _users,
			};

			return View(viewModel);
		}

		//Metod för sökning på användare 
		//Anropas via Ajax
		[Authorize]
		[HttpPost]
		public IActionResult Search(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				return RedirectToAction("Start");
			}

			AppUser _loggedInUser = GetLoggedInUser(); 

			//Tarbort användare som inte är aktiva eller har privat åtkomst
			List<AppUser> _users = _dbContext.Users.Where(user => user.Firstname.Contains(searchString) && user.Id != _loggedInUser.Id && user.Active && user.ProfileAccess).ToList();

			var viewModel = new SearchViewModel
			{
				LoggedInUser = _loggedInUser,
				Users = _users,
				SearchString = searchString
			};

			return View("Start", viewModel);
		}

		//Metod för att Läsa in en konversation mellan två användare
		//Anropas via Ajax
		[Authorize]
		[HttpGet]
		public IActionResult GetConversation(string otherUserID)
		{
			string currentUserId = GetLoggedInUser().Id;

			//Ifall användaren id som skickas med är korrekt emot databasen
			if (_dbContext.Users.Find(otherUserID) != null)
			{
				//Hämtar konversationen mellan det två deltagarna och lägger dem i en lista
				List<Message> conversation = _dbContext.Messages.Where(received => received.FromUserID == otherUserID && received.ToUserID == currentUserId).ToList();
				conversation.AddRange(_dbContext.Messages.Where(sent => sent.ToUserID == otherUserID && sent.FromUserID == currentUserId).ToList());

				conversation = conversation.OrderBy(message => message.MessageId).ToList();
				List<Message_object> list_objects = new List<Message_object>();

				//Lägger på information om meddelandet för att hantera css klasser i html sidan 
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

					// Använd AutoMapper för att mappa entiteterna 
					var config = new MapperConfiguration(cfg => cfg.CreateMap<Message, Message_object>());
					var mapper = new Mapper(config);
					mess_obj = mapper.Map<Message_object>(message);

					//Uppdaterar modellen 
					mess_obj.classCss = messdir;
					mess_obj.MessageName = messName;
					mess_obj.MessageId = message.MessageId;
					mess_obj.Text = message.Text;

					list_objects.Add(mess_obj);
				}
				//Skapar en viewModel där varje meddelande ska läggas i
				MessageServiceModel data = new MessageServiceModel();
				data.messagesObjects = list_objects;
				data.receiver = _dbContext.Users.Find(otherUserID);

				//Ifall man har en konversation visas konversationen annars visas nytt meddelande sida
				if (data.messagesObjects != null && data.messagesObjects.Any())
				{
					return PartialView("_ConversationPartial", data);
				}
				else
				{
					MessageServiceModel dataNewMessage = new MessageServiceModel();
					dataNewMessage.receiver = _dbContext.Users.Find(otherUserID);

					return PartialView("_NewMessagePartial", dataNewMessage);
				}
			}
			else
			{
				MessageServiceModel dataUnknown = new MessageServiceModel();
				List<Message> Messages = GetLoggedInUser().ReceivedMessages.Where(m => m.UnknownUser + m.Text.GetHashCode() == otherUserID).ToList();
				dataUnknown.UnknonwMessages = Messages;

				return PartialView("_UnknownMessages", dataUnknown);
			}
		}

		//Anropas via Ajax
		[HttpPost]
		public IActionResult Send(string Newmessage, string receiverId)
		{
			if (ModelState.IsValid)
			{
                Message message = new Message();
                message.Text = Newmessage;
                message.ToUserID = receiverId;
                message.Datum = DateTime.Now;
                message.FromUserID = userManager.GetUserAsync(User).Result.Id;
                message.ViewStatus = false;

                _dbContext.Add(message);
                _dbContext.SaveChanges();
            }
			

			return RedirectToAction("GetConversation", new {otherUserId = receiverId });
		}

		//Metod för att skicka meddelanden ifall man inte är en användare. Anropas via Ajax
		[HttpPost]
		public IActionResult SendUnkown(string newmessage, string receiverId, string from)
		{
            if (ModelState.IsValid)
            {
                Message message = new Message();
                message.Text = newmessage;
                message.ToUserID = receiverId;
                message.Datum = DateTime.Now;
				message.UnknownUser = from;
                message.ViewStatus = false;

                _dbContext.Add(message);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index", "Resume", new { id = receiverId });
        }

		//metod för att markera meddelanden som lästa. Anropas via Ajax
		[Authorize]
		[HttpPost]
		public IActionResult MarkRead(int _messageID)
		{
			int id = _messageID;
			Message message = _dbContext.Messages.Find(id);
			message.ViewStatus = true;

			_dbContext.Messages.Update(message);
			_dbContext.SaveChanges();

			return RedirectToAction("GetConversation", new { otherUserId = message.ToUserID });

		}

		[HttpGet]
		public Boolean IsLoggedIn()
		{
			return User.Identity.IsAuthenticated;
		}

		private AppUser GetLoggedInUser()
		{
			AppUser loggedInUser = userManager.GetUserAsync(User).Result;
			return loggedInUser;
			
		}

		//Anropas via Ajax
		//Kollar hur många meddelanden som man har olästa. 
		[HttpGet]
		public IActionResult UnreadMessages()
		{
			if (User.Identity.IsAuthenticated)
			{
                AppUser myuser = GetLoggedInUser();
                int unreadMessages = myuser.ReceivedMessages.Where(m => m.ViewStatus == false && m.fkFromUser.Active).Count();
                return Json(unreadMessages);
            } else
			{
				return Json(null);
			}
		}

		//metod för att skapa användare för meddelanen som inte har inloggade användare
		private List<AppUser> GetUnknonwUsers(AppUser _signedInUser)
		{
			List<Message> unknownMessages = _signedInUser.ReceivedMessages.Where(m => m.fkFromUser == null && m.UnknownUser != null).ToList();
			List<AppUser> unknownUsers = new List<AppUser>();

			foreach(Message m in unknownMessages)
			{
				AppUser newUser = new AppUser();
				
				//Skapar ett unikt id
				newUser.Id = m.UnknownUser + m.Text.GetHashCode();
				newUser.Firstname = m.UnknownUser;
				unknownUsers.Add(newUser);
			}

			return unknownUsers;
		}

	}
}
