using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Web;
using SkillSkulptor.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using AspNetCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace SkillSkulptor.Controllers
{
	public class LoginController : Controller
	{
		private UserManager<AppUser> userManager;
		private SignInManager<AppUser> signInManager;

		public LoginController (UserManager<AppUser> userMngr,
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
		public ActionResult Signup()
		{
			return View();
		}
		[HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
			if(ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(
					loginViewModel.Email,
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
			//bool userExists = context.AppUsers.Any(o => o.Email == credentials.Email && o.Password == credentials.Password);
			//AppUser user = context.AppUsers.FirstOrDefault(o => o.Email == credentials.Email && o.Password == credentials.Password);

			//if (userExists)
			//{
			//	var claims = new List<Claim>
			//		{
			//		new Claim(ClaimTypes.Name, user.Firstname)
			//	};
			//	var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			//	var authProperties = new AuthenticationProperties
			//	{

			//	};


   //             await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

			//	return RedirectToAction("Index", "Home");
			
			//if (userExists)
			//{
			//             FormAuthentication.SetAuthCookie(user.Firstname, false);
			//	return RedirectToAction("Index", "Home");	
			//}
			//ModelState.AddModelError("", "Email eller lösenord är felaktigt");
			//return View();
		}

        public ActionResult Signout()
		{
			return View();
		}


    }
}
