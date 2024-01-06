using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSkulptor.Models;
using System.Linq;
using System.Security.Claims;
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

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
			AppUser user = userManager.GetUserAsync(User).Result;
            ProfileViewModel model = MapToProfileViewModel(user);

            ViewBag.fkPicture = user.fkPicture;

			return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
			AppUser user = userManager.GetUserAsync(User).Result;
            PasswordViewModel pwModel = new PasswordViewModel();

            return View(pwModel);
		}

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordViewModel pwModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = userManager.GetUserAsync(User).Result;

                try
                {
                    var result = await userManager.ChangePasswordAsync(user, pwModel.OldPassword, pwModel.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Profile");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View(pwModel);

                }
            }
            else
            {
                return View(pwModel);
            }

            return View(pwModel);

        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(ProfileViewModel model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Hämta den befintliga användaren från databasen
                    var _existingUser = await _dbContext.Users.FindAsync(model.Id);

                    if (_existingUser == null)
                    {
                        // Hantera fallet då användaren inte hittades
                        return View("Index", "Home");
                    }

                    _existingUser.Firstname = model.Firstname;

                    _existingUser.Lastname = model.Lastname;

                    _existingUser.Email = model.Email;

                    _existingUser.Phonenr = model.Phonenr;

                    _existingUser.Active = model.Active;

                    _existingUser.ProfileAccess = model.ProfileAccess;

                    _existingUser.fkAddress.Street = model.Street;

                    _existingUser.fkAddress.City = model.City;

                    _existingUser.fkAddress.ZipCode = model.ZipCode;

                    _existingUser.fkAddress.Country = model.Country;


                    if (file != null && file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);

                            if (_existingUser.fkPicture == null)
                            {
                                _existingUser.fkPicture = new Profilepicture(); // Ersätt 'Profilepicture' med korrekt klassnamn
                            }

                            _existingUser.fkPicture.ImageData = stream.ToArray();
                            _existingUser.fkPicture.Filename = file.FileName;
                        }
                    }

                    ViewBag.fkPicture = _existingUser.fkPicture;
                    var result = await _dbContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Index", "Error");
                }
            }
            else
            {
                //ifall inget ändrats skickas användaren tillbaka till redigera sidan
                return View("Index", model);
            }
            return View("Index", model);
        }


        public async Task<IActionResult> UserImage(string userId)
        {
            var userPicture = await _dbContext.ProfilePictures
                                              .FirstOrDefaultAsync(p => p.pictureUser.Id == userId);

            if (userPicture?.ImageData != null)
            {
                // Typen JPEG, uppdatera MIME-typen enligt bildformatet
                return File(userPicture.ImageData, "image/jpeg");
            }


            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "datafiles", "pictures", "default-profile.jpg");
            var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

            
            var mimeType = "image/jpeg"; 

            return File(imageBytes, mimeType);

        }

        private ProfileViewModel MapToProfileViewModel(AppUser user)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppUser, ProfileViewModel>()
                    .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.fkAddress.Street))
                    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.fkAddress.City))
                    .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.fkAddress.ZipCode))
                    .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.fkAddress.Country));
            });

            var mapper = new Mapper(config);

            // Använd AutoMapper för att kartlägga egenskaperna
            return mapper.Map<ProfileViewModel>(user);
        }
    }
}

