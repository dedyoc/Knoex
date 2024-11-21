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
        Task<int> AddAnswerToPostAsync(Post post, Post answer, User user);
        Task<int> UpdateVoteScoreAsync(Post post, int score);
        Task<int> UpdateCountersAsync(Post post);

        Task<int> RegisterViewAsync(Post post, User user);
        Task<int> AcceptAnswerAsync(Post post, Post answer);
        Task<PagedResult<Post>> GetPostsByUserAsync(User user, int page = 1);
    }
}