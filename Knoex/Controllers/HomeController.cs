using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Knoex.Models;
using Knoex.Repositories;
using Knoex.Data;
using Knoex.ViewModels;

namespace Knoex.Controllers;

public class HomeController : CommonController
{
    private readonly IPostRepository _postRepository;

    public HomeController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpGet("/")]
    public async Task<IActionResult> Index(int page = 1, string filter = "newest")
    {
        PagedResult<Post> posts = filter switch
        {
            "newest" => await _postRepository.GetPostsAsync(page),
            "unanswered" => await _postRepository.GetUnansweredPostsAsync(page),
            "activity" => await _postRepository.GetRecentActivityPostsAsync(page),
            _ => await _postRepository.GetPostsAsync(page)
        };

        return View(new HomeViewModel
        {
            CurrentFilter = filter,
            Posts = posts
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
