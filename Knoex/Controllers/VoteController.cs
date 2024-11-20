using Knoex.Models;
using Knoex.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public class VoteController : CommonController
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public VoteController(IVoteRepository voteRepository, IPostRepository postRepository, IUserRepository userRepository)
        {
            _voteRepository = voteRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/questions/{postId}/vote/{type}")]
        public async Task<IActionResult> Vote(int postId, VoteType type)
        {
            var post = await _postRepository.GetPostByIdAsync(postId);
            User user = await _userRepository.GetCurrentUserAsync();
            var vote = _voteRepository.AddVoteAsync(post, user, type);
            var score = _voteRepository.GetVoteScoreAsync(post);
            await _postRepository.UpdateVoteScoreAsync(post, score);
            return Json(new { score, vote }); //update the UI via JS on Alpine.js
        }
    }
}