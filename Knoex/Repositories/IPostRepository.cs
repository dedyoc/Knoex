using Knoex.Data;
using Knoex.Models;

namespace Knoex.Repositories
{
    public interface IPostRepository
    {
        Task<PagedResult<Post>> GetPostsAsync(int page = 1, int pageSize = 10);
        Task<PagedResult<Post>> GetUnansweredPostsAsync(int page = 1, int pageSize = 10);
        Task<PagedResult<Post>> GetRecentActivityPostsAsync(int page = 1, int pageSize = 10);

        Task<Post> GetPostByIdAsync(int id);
        Task<Post> GetPostWithDetailsAsync(int id);

        Task<int> CreatePostAsync(Post post, User user);
        Task<int> AddTagToPostAsync(Post post, string[] tags);
        Task<int> AddAnswerToPostAsync(Post post, Post anwser, User user);

    }
}