using Knoex.Data;
using Knoex.Models;
using Microsoft.EntityFrameworkCore;

namespace Knoex.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly KnoexContext _context;
        public SearchRepository(KnoexContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Post>> SearchPostAsync(string query, int page = 1)
        {
            var tag = await _context.Tags
                .Where(t => t.Name.Contains(query))
                .FirstOrDefaultAsync();
            return await _context.Posts
                .Where(p => p.ParentId == null)
                .Where(p => p.SearchVector.Matches(query) || p.Tags.Contains(tag))
                .Include(p => p.Tags)
                .Include(p => p.User)
                .GetPagedAsync(page, 10);
        }
    }
}