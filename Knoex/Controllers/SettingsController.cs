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
        public async Task<RedirectResult> Save(string email, string firstName, string lastName, string currentPassword, string newPassword, string url)
        {
            var user = await _userRepository.GetCurrentUserAsync();
            var result = await _signInManager.CheckPasswordSignInAsync(user, currentPassword, false);
            if (result.Succeeded)
            {
                await _userRepository.UpdateUserAsync(user.Id, email, firstName, lastName);
                if (newPassword != null) await _userRepository.UpdatePasswordAsync(user, currentPassword, newPassword);
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