using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskSurvey.Models;
using TaskSurvey.ViewModel;

namespace TaskSurvey.Controllers
{
    public class HomeController : Controller
    {
		private readonly UserManager<Users> _userManager;
		private readonly SignInManager<Users> _signInManager;

		public HomeController(UserManager<Users> userManager,SignInManager<Users> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;

		}
		
        //Home page
        public IActionResult Index()
        {
            return View();
        }
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new Users { UserName = model.Email, Email = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					return RedirectToAction("Login", "Home");
				}
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
  
                }


            }

			return View(model);
		}
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(await _userManager.GetUserAsync(User));

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Analytics", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("SurveyQuestions", "Survey");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
