using Knoex.Data;
using Knoex.Models;

namespace Knoex.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly KnoexContext _context;
        public CommentRepository(KnoexContext context)
        {
            _context = context;
        }
        public async Task<int> AddCommentToPostAsync(Post post, Comment comment, User user)
        {
            comment.User = user;
            comment.Post = post;
            _context.Comments.Add(comment);
            return await _context.SaveChangesAsync();
        }
    }
}