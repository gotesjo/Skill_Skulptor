﻿using Microsoft.AspNetCore.Identity;
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
		private UserManager<AppUser> userManager;
		private IWebHostEnvironment _hostingEnvironment;

        public ProfileController(SsDbContext db, IWebHostEnvironment hostingEnvironment, UserManager<AppUser> _userManager)
        {
            _dbContext = db;
			userManager = _userManager;
			_hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
			AppUser user = userManager.GetUserAsync(User).Result;

			return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
			AppUser user = userManager.GetUserAsync(User).Result;

			return View(user);
		}

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(AppUser user, IFormFile file)
        {
            try
            {
                // Hämta den befintliga användaren från databasen
                var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);

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
                if (!string.IsNullOrEmpty(user.PasswordHash))
                {
                    // Om ett nytt lösenord har angetts, uppdatera lösenordet
                    existingUser.PasswordHash = user.PasswordHash;
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

        public async Task<IActionResult> UserImage(string userId)
        {
            var userPicture = await _dbContext.ProfilePictures
                                              .FirstOrDefaultAsync(p => p.pictureUser.Id == userId);

            if (userPicture?.ImageData != null)
            {
                // Anta att bilden är av typen JPEG, uppdatera MIME-typen enligt bildformatet
                return File(userPicture.ImageData, "image/jpeg");
            }


            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "datafiles", "pictures", "default-profile.jpg");
            var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            // Ange rätt MIME-typ beroende på filtypen
            var mimeType = "image/jpeg"; // Om din bild är en JPEG. Anpassa efter din bildtyp.

            return File(imageBytes, mimeType);

        }


    }
}

