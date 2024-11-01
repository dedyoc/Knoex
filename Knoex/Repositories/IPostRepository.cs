using Knoex.Models;

namespace Knoex.Repositories
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPostsAsync();
        Task<int> CreatePostAsync(Post post, User user);
        Task<int> AddTagToPostAsync(Post post, string[] tags);
    }
}