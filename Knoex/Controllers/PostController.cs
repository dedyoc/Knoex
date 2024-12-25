using Knoex.Models;
using Knoex.Repositories;
using Knoex.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public class PostController : CommonController
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IVoteRepository _voteRepository;
        public PostController(IPostRepository postRepository, IUserRepository userRepository, IVoteRepository voteRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _voteRepository = voteRepository;
        }
        [Authorize]
        [HttpGet("/ask")]
        public IActionResult New()
        {
            return View();
        }
        /// <summary>
        /// Handles the creation of a new post.
        /// </summary>
        /// <param name="post">The post object containing the title and body.</param>
        /// <param name="tags">An array of tags associated with the post.</param>
        /// <returns>An IActionResult indicating the result of the action.</returns>
        /// <remarks>
        /// The ModelState is used to check if the incoming model (post) is valid.
        /// If the model is not valid, the method returns the view with validation errors.
        /// </remarks>
        [Authorize]
        [HttpPost("/ask")]
        public async Task<IActionResult> New([Bind("Title,Body")] Post post, string[] tags)
        {

            User user = await _userRepository.GetCurrentUserAsync();
            await _postRepository.CreatePostAsync(post, user);
            await _postRepository.AddTagToPostAsync(post, tags.Take(5).ToArray());
            AddNotification("Success", "Your question is successfully created");
            return Redirect("/");
            // return View();
        }


        [HttpGet("/questions/{id}")]
        public async Task<IActionResult> Post(int id)
        {
            var post = await _postRepository.GetPostWithDetailsAsync(id);
            if (post == null) return NotFound();
            var related = _postRepository.GetRelatedPosts(post);
            if (User.Identity.IsAuthenticated)
            {
                User user = await _userRepository.GetCurrentUserAsync();
                List<Vote> votes = await _voteRepository.GetGivenVotes(post, user);
                await _postRepository.RegisterViewAsync(post, user);
                return View(new PostViewModel
                {
                    Post = post,
                    GivenVotes = votes,
                    RelatedPosts = related
                });
            }
            else
            {
                return View(new PostViewModel
                {
                    Post = post,
                    RelatedPosts = related
                });
            }
        }

        [Authorize]
        [HttpPost("/questions/{id}/answer")]
        public async Task<IActionResult> Answer(int id, [Bind("Body")] Post answer)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null || post.ParentId != null) return NotFound();
            if (ModelState.IsValid)
            {
                User user = await _userRepository.GetCurrentUserAsync();
                await _postRepository.AddAnswerToPostAsync(post, answer, user);
                AddNotification("Success", "Your answer is successfully added");
            }
            return RedirectToAction(nameof(Post), new { id = post.Id });
        }

        [Authorize]
        [HttpPost("/questions/{id}/accept/{answerId}")]
        public async Task<IActionResult> AcceptAnswer(int id, int answerId)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            var answer = await _postRepository.GetPostByIdAsync(answerId);
            if (post == null || answer == null || post.ParentId != null || answer.ParentId != post.Id) return NotFound();
            User user = await _userRepository.GetCurrentUserAsync();
            if (post.UserId != user.Id) return Forbid();
            await _postRepository.AcceptAnswerAsync(post, answer);
            AddNotification("Success", "Answer accepted successfully");
            foreach (Post a in post.Answers)
            {
                await _postRepository.UpdateCountersAsync(a);
            }
            return RedirectToAction(nameof(Post), new { id = post.Id });
        }
    }
}