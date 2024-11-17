using Knoex.Models;
using Knoex.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public class PostCommentController : CommonController
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        public PostCommentController(
            IPostRepository postRepository,
            IUserRepository userRepository,
            ICommentRepository commentRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        [Authorize]
        [HttpPost("/questions/{id}/comments")]
        public async Task<IActionResult> Anwser(int id, [Bind("Body")] Comment comment)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            if (ModelState.IsValid)
            {
                User user = await _userRepository.GetCurrentUserAsync();
                await _commentRepository.AddCommentToPostAsync(post, comment, user);
                AddNotification("Success", "Your comment is successfully added");
            }
            var returnPostId = post.ParentId ?? post.Id;
            return RedirectToAction(nameof(PostController.Post), "Post", new { id = returnPostId });
        }
    }
}