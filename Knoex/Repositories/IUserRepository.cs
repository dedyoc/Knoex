
using Knoex.Models;

namespace Knoex.Repositories
{
    public interface IUserRepository
    {
        User GetCurrentUser();

        Task<User> GetCurrentUserAsync();
    }
}