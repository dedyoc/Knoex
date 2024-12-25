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
        public Task<PagedResult<Post>> GetPostsAsync(int page = 1, int pageSize = 10)
        {
            return _context.Posts
                .Where(p => p.ParentId == null)
                .Include(p => p.Tags)
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .GetPagedAsync(page, pageSize);
        }

        public Task<PagedResult<Post>> GetUnansweredPostsAsync(int page = 1, int pageSize = 10)
        {
            return _context.Posts
                .Where(p => p.ParentId == null && p.AcceptedAnswerId == null)
                .Include(p => p.Tags)
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .GetPagedAsync(page, pageSize);
        }

        public Task<PagedResult<Post>> GetRecentActivityPostsAsync(int page = 1, int pageSize = 10)
        {
            return _context.Posts
                .Where(p => p.ParentId == null)
                .Include(p => p.Tags)
                .Include(p => p.User)
                .OrderByDescending(p => p.UpdatedAt)
                .GetPagedAsync(page, pageSize);
        }

        public Task<Post> GetPostByIdAsync(int id)
        {
            return _context.Posts
                .Include(p => p.User)
                .Where(p => p.Id == id)
                .FirstAsync();
        }
        public Task<Post> GetPostWithDetailsAsync(int id)
        {
            return _context.Posts
                .Where(p => p.ParentId == null)
                .Where(p => p.Id == id)
                .Include(p => p.User)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                    .ThenInclude(comments => comments.User) // Comment's user
                .Include(p => p.Answers)
                    .ThenInclude(anwsers => anwsers.User) // Answer's user
                .Include(p => p.Answers)
                    .ThenInclude(answers => answers.Comments)
                        .ThenInclude(answerComments => answerComments.User) // Answer's comments user
                .AsSplitQuery()
                .FirstAsync();
        }
        public List<Post> GetRelatedPosts(Post post, int amount = 10)
        {
            List<Post> posts = new();
            post.Tags.ToList().ForEach(tag =>
            {
                var result = _context.Posts
                    .Where(p => p.Id != post.Id)
                    .Where(p => p.ParentId == null)
                    .Where(p => p.Tags.Contains(tag))
                    .Include(p => p.Tags)
                    .OrderByDescending(p => p.VoteScore)
                    .ThenBy(p => p.ViewCount)
                    .Take(amount * 5)
                    .ToList();
                result.ForEach(p => posts.Add(p));
            });
            return posts.GroupBy(p => p.Id)
                .Select(p => new { Post = p.First(), Count = p.Count() })
                .OrderByDescending(p => p.Count)
                .Take(amount)
                .Select(x => x.Post)
                .ToList();
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

        public async Task<int> AddAnswerToPostAsync(Post post, Post answer, User user)
        {
            answer.Type = PostType.Answer;
            answer.UserId = user.Id;
            answer.ParentId = post.Id;
            post.UpdatedAt = DateTime.UtcNow;

            _context.Posts.Update(post);

            _context.Posts.Add(answer);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> UpdateVoteScoreAsync(Post post, int score)
        {
            post.VoteScore = score;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateCountersAsync(Post post)
        {
            post.AnswersCount = _context.Posts.Count(p => p.ParentId == post.Id);
            post.ViewCount = _context.Views.Count(v => v.PostId == post.Id);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RegisterViewAsync(Post post, User user)
        {
            var view = _context.Views
                .Where(v => v.PostId == post.Id && v.UserId == user.Id)
                .FirstOrDefault();
            if (view == null) _context.Views.Add(new View { PostId = post.Id, UserId = user.Id });
            else view.UpdateTimestamp();
            await _context.SaveChangesAsync();
            return await UpdateCountersAsync(post);
        }

        public async Task<int> AcceptAnswerAsync(Post post, Post answer)
        {
            post.AcceptedAnswerId = answer.Id;
            post.UpdatedAt = DateTime.UtcNow;
            return await _context.SaveChangesAsync();
        }

        public Task<PagedResult<Post>> GetPostsByUserAsync(User user, int page = 1)
        {
            return _context.Posts
                .Where(p => p.UserId == user.Id)
                .Where(p => p.ParentId == null)
                .Include(p => p.Tags)
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .GetPagedAsync(page, 10);
        }

    }
}