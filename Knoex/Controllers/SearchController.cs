using Knoex.Repositories;
using Knoex.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public class SearchController : CommonController
    {
        private readonly ISearchRepository _searchRepository;
        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }
        [HttpGet("/search")]
        public async Task<IActionResult> Index(string q = "", int page = 1)
        {
            return View(new SearchViewModel
            {
                Posts = await _searchRepository.SearchPostAsync(q, page)
            });
        }
    }
}