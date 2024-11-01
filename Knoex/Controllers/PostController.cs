using Knoex.Models;
using Knoex.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public class PostController : CommonController
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
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
            await _postRepository.AddTagToPostAsync(post, tags.Take(3).ToArray());
            AddNotification("Success", "Your question is successfully created");
            return Redirect("/");
            // return View();
        }
    }
}