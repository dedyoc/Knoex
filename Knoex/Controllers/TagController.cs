using Knoex.Repositories;
using Knoex.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public class TagController : CommonController
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        [HttpGet("/tags")]
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(new TagViewModel
            {
                Tags = await _tagRepository.GetTagsAsync(page)
            });
        }
    }
}