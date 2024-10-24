using Knoex.Models;
using Knoex.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public class SettingsController : CommonController
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        public SettingsController(IUserRepository userRepository, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }
        [Authorize]
        [HttpPost("/settings")]
        public async Task<RedirectResult> Save(string email, string first_name, string last_name, string current_password, string new_password, string url)
        {
            var user = await _userRepository.GetCurrentUserAsync();
            var result = await _signInManager.CheckPasswordSignInAsync(user, current_password, false);
            if (result.Succeeded)
            {
                await _userRepository.UpdateUserAsync(user.Id, email, first_name, last_name);
                if (new_password != null) await _userRepository.UpdatePasswordAsync(user, current_password, new_password);
                AddNotification("Success", "Settings updated successfully");
            }
            else
            {
                AddNotification("Error", "Incorrect password", false);
            }
            return Redirect(url);
        }
    }
}