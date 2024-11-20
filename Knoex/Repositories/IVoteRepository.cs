using Knoex.Models;

namespace Knoex.Repositories
{
    public interface IVoteRepository
    {
        Task<List<Vote>> GetGivenVotes(Post post, User user);
        int GetVoteScoreAsync(Post post);
        object AddVoteAsync(Post post, User user, VoteType type);
    }
}