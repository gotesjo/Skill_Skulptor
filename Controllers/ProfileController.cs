using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSkulptor.Models;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SkillSkulptor.Controllers
{
	public class ProfileController : Controller
	{
		private SsDbContext _dbContext;

		public ProfileController(SsDbContext db)
		{
			_dbContext = db;
		}

		[HttpGet]
		public IActionResult Index()
		{
			// Hämta alla projekt från databasen
			var user = _dbContext.AppUsers.FirstOrDefault(u => u.UserId == 1);

			if (user == null)
			{
				return NotFound();
			}



			return View(user);
		}

		[HttpPost]
		public IActionResult ProfileEdit(AppUser user)
		{
            try
            {
                // Hämta den befintliga användaren från databasen
                var existingUser = _dbContext.AppUsers.FirstOrDefault(u => u.UserId == user.UserId);


                // Uppdatera övriga egenskaper (förnamn, efternamn, e-post etc.)
                existingUser.Firstname = user.Firstname;
                existingUser.Lastname = user.Lastname;
                existingUser.Email = user.Email;
                existingUser.Phonenr = user.Phonenr;
                existingUser.fkAddress.Street = user.fkAddress.Street;
                existingUser.fkAddress.City = user.fkAddress.City;
                existingUser.fkAddress.ZipCode = user.fkAddress.ZipCode;
                existingUser.fkAddress.Country = user.fkAddress.Country;
            


                // Kontrollera om ett nytt lösenord har angetts
                if (!string.IsNullOrEmpty(user.Password))
                    {
                        // Om ett nytt lösenord har angetts, uppdatera lösenordet
                        existingUser.Password = user.Password;
                    }

            _dbContext.Update(existingUser);
            _dbContext.SaveChanges();

            return View("Index", user); // Ladda om Index-vyn med uppdaterade data
                

            }
            catch (Exception ex)
            {
                // Logga felmeddelandet eller använd debugverktyg för att undersöka problemet.
                // Du kan också använda ex.Message för att få mer information om felet.
                return RedirectToAction("Index", "Error"); // Visa en felvy
            }
        }
                

    }
}

