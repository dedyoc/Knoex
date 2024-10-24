using Knoex.Data;
using Knoex.Models;
using Knoex.Repositories;
using Microsoft.AspNetCore.Identity;
namespace Knoex.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KnoexContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(KnoexContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetCurrentUser()
        {
            var id = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _context.Users.Find(int.Parse(id));
        }

        public Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }

        public Task<int> UpdateUserAsync(int id, string email, string firstName, string lastName)
        {
            var user = _context.Users.Where(u => u.Id == id).First();
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            return _context.SaveChangesAsync();
        }

        public async Task<IdentityResult> UpdatePasswordAsync(User user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
    }
}