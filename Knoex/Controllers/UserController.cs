using Knoex.Repositories;
using Knoex.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public class UserController : CommonController
    {
        private readonly IUserRepository _UserRepository;
        private readonly IPostRepository _postRepository;
        public UserController(IUserRepository userRepository, IPostRepository postRepository)
        {
            _UserRepository = userRepository;
            _postRepository = postRepository;
        }
        [HttpGet("/users")]
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(new UserIndexViewModel
            {
                Users = await _UserRepository.GetUsersAsync(page)
            });
        }
        [HttpGet("/users/{id}")]
        public async Task<IActionResult> Detail(int id, int page = 1)
        {
            var user = await _UserRepository.GetUserByIdAsync(id);
            var posts = await _postRepository.GetPostsByUserAsync(user);
            return View(new UserDetailViewModel { User = user, Posts = posts });
        }
    }
}