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
    }
}