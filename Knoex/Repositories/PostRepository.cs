using Knoex.Data;
using Knoex.Models;
using Microsoft.EntityFrameworkCore;

namespace Knoex.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly KnoexContext _context;
        private readonly ITagRepository _tagRepository;
        public PostRepository(KnoexContext context, ITagRepository tagRepository)
        {
            _context = context;
            _tagRepository = tagRepository;
        }
        public Task<List<Post>> GetPostsAsync()
        {
            return _context.Posts
                .Where(p => p.ParentId == null)
                .Include(p => p.Tags)
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
        public Task<int> CreatePostAsync(Post post, User user)
        {
            post.Type = (PostType)(int)PostType.Question;
            post.UserId = user.Id;
            _context.Posts.Add(post);
            return _context.SaveChangesAsync();
        }
        public async Task<int> AddTagToPostAsync(Post post, string[] tags)
        {
            foreach (var tag in tags)
            {
                var tagEntity = await _tagRepository.GetOrCreateTagAsync(tag);
                post.Tags.Add(tagEntity);
            }
            return await _context.SaveChangesAsync();
        }
    }
}