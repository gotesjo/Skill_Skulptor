using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Web;
using SkillSkulptor.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SkillSkulptor.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUser> userManager;
		private SignInManager<AppUser> signInManager;

		public AccountController (UserManager<AppUser> userMngr,
			SignInManager<AppUser> signInMngr)
		{
			this.userManager = userMngr;
			this.signInManager = signInMngr;
		}
		[HttpGet]
		public ActionResult Login()
		{
			LoginViewModel loginViewModel = new LoginViewModel();
			return View(loginViewModel);
		}


		[HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
			if(ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(
					loginViewModel.Username,
					loginViewModel.Password,
					isPersistent: loginViewModel.RemeberMe,
					lockoutOnFailure: false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Du har angett fel Email eller lösenord");
				}
			}
			return View(loginViewModel);

		}

		[HttpGet]
        public async Task<ActionResult> Signout()
		{
			await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
		public ActionResult SignUp()
        {
            UserRegistrationViewModel userRegistrationViewModel = new UserRegistrationViewModel();
            return View(userRegistrationViewModel);
        }

		[HttpPost]
		public async Task<IActionResult> SignUp(UserRegistrationViewModel _urm)
		{
			if (ModelState.IsValid)
			{
                AppUser appUser = new AppUser();
				appUser.UserName = _urm.Username;
                appUser.Email = _urm.Email;
                appUser.Firstname = _urm.Firstname;
                appUser.Lastname = _urm.Lastname;

                var result = await userManager.CreateAsync(appUser, _urm.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(appUser, isPersistent: true);
                    return RedirectToAction("Index", "Home");

                }

            }

			return View(_urm);
		}

	
    }
}
