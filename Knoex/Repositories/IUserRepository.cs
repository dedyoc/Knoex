
using Knoex.Data;
using Knoex.Models;
using Microsoft.AspNetCore.Identity;

namespace Knoex.Repositories
{
    public interface IUserRepository
    {
        User GetCurrentUser();
        Task<User> GetCurrentUserAsync();
        Task<int> UpdateUserAsync(int id, string email, string first_name, string last_name);
        Task<IdentityResult> UpdatePasswordAsync(User user, string currentPassword, string newPassword);
        Task<int> UpdateCountersAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<PagedResult<User>> GetUsersAsync(int page = 1);
    }
}