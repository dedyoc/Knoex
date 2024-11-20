using Knoex.Data;
using Knoex.Models;
using Microsoft.EntityFrameworkCore;

namespace Knoex.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly KnoexContext _context;
        public VoteRepository(KnoexContext context)
        {
            _context = context;
        }
        /// <summary>
        /// This function is used to display the votes given by the user to the post.
        /// Including the votes given to the answers of the post.
        /// </summary>
        /// <param name="post"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<List<Vote>> GetGivenVotes(Post post, User user)
        {
            List<int> ids = post.Answers.Select(a => a.Id).ToList();
            ids.Add(post.Id);
            return _context.Votes
                .Where(v => ids.Contains(v.PostId))
                .Where(v => v.UserId == user.Id)
                .ToListAsync();
        }
        public int GetVoteScoreAsync(Post post)
        {
            return _context.Votes.Where(v => v.Post == post).Sum(v => v.Type == VoteType.Upvote ? 1 : -1);
        }
        public object AddVoteAsync(Post post, User user, VoteType type)
        {
            var vote = _context.Votes
                .Where(v => v.PostId == post.Id && v.UserId == user.Id)
                .FirstOrDefault();
            if (vote != null) // if vote already exists
            {
                _context.Remove(vote);
                _context.SaveChanges();
                if (vote.Type == type) return null; // simply exit if the vote type is the same
            }
            vote = new Vote
            {
                Post = post,
                User = user,
                Type = type
            };
            _context.Votes.Add(vote);
            _context.SaveChanges();
            return vote.Type;
        }
    }
}