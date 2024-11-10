using Knoex.Data;
using Knoex.Models;

namespace Knoex.Repositories
{
    public interface IPostRepository
    {
        Task<PagedResult<Post>> GetPostsAsync(int page = 1, int pageSize = 10);
        Task<PagedResult<Post>> GetUnansweredPostsAsync(int page = 1, int pageSize = 10);
        Task<PagedResult<Post>> GetRecentActivityPostsAsync(int page = 1, int pageSize = 10);
        Task<int> CreatePostAsync(Post post, User user);
        Task<int> AddTagToPostAsync(Post post, string[] tags);
    }
}