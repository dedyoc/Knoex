using Knoex.Models;
using Knoex.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Knoex.Controllers
{
    public class AuthenticationController : CommonController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("/login")]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(string username, string password, bool remember, string returnUrl)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, remember, false);
                if (signInResult.Succeeded)
                {
                    AddNotification("Login Successful", "You have successfully logged in.");
                    return string.IsNullOrEmpty(returnUrl)
                        ? RedirectToAction(nameof(HomeController.Index), "Home")
                        : Redirect(returnUrl);
                }
            }
            return View(new LoginViewModel { LoginFailed = true });
        }

        [Authorize]
        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(string email, string username, string password, string firstName, string lastName)
        {
            var user = new User
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                AddNotification("Registration Successful", "You have successfully registered an account.");
                return RedirectToAction(nameof(Login));
            }
            AddNotification("Registration Failed", "There was an error registering your account.", false);
            return RedirectToAction(nameof(Register));
        }
    }
}
