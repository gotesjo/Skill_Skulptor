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
        private IWebHostEnvironment _hostingEnvironment;

        public ProfileController(SsDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = db;
            _hostingEnvironment = hostingEnvironment;
        }

		[HttpGet]
		public IActionResult Index()
		{
            var user = _dbContext.AppUsers.FirstOrDefault(u => u.UserId == 1);

			return View(user);
		}

        [HttpGet]
        public IActionResult ChangePassword() 
        {
            var user = _dbContext.AppUsers.FirstOrDefault(u => u.UserId == 1);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(AppUser user, IFormFile file)
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
                existingUser.Active = user.Active;
                existingUser.ProfileAccess = user.ProfileAccess;
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

                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        if(existingUser.fkPicture != null)
                        {
                            existingUser.fkPicture.ImageData = stream.ToArray();
                            existingUser.fkPicture.Filename = file.FileName;
                        }
                        else
                        {
                            existingUser.fkPicture = new Profilepicture
                            {
                                Filename = file.FileName,
                                ImageData = stream.ToArray()
                            };
                        }

                    }
                }

                _dbContext.Update(existingUser);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Error"); // Visa en felvy
            }
        }

        public async Task<IActionResult> UserImage(int userId)
        {
            var userPicture = await _dbContext.ProfilePictures
                                              .FirstOrDefaultAsync(p => p.pictureUser.UserId == userId);

            if (userPicture?.ImageData != null)
            {
                // Anta att bilden är av typen JPEG, uppdatera MIME-typen enligt bildformatet
                return File(userPicture.ImageData, "image/jpeg");
            }

            
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "default-profile.png");
            var imageBytes = await System.IO.File.ReadAllBytesAsync(path);
            return File(imageBytes, "image/png"); // Anpassa MIME-typen enligt standardbilden
        }


    }
}

