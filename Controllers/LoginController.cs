using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using SkillSkulptor.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

namespace SkillSkulptor.Controllers
{
	public class LoginController : Controller
	{

		public ActionResult Login()
		{
			return View();
		}
		public ActionResult Signup()
		{
			return View();
		}
		public ActionResult Signout()
		{
			return View();
		}
	
	}
}
