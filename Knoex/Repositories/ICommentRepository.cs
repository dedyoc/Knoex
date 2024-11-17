using Knoex.Models;

namespace Knoex.Repositories
{
    public interface ICommentRepository
    {
        Task<int> AddCommentToPostAsync(Post post, Comment comment, User user);
    }
}