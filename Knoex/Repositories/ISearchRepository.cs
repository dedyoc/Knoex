using Knoex.Data;
using Knoex.Models;

namespace Knoex.Repositories
{
    public interface ISearchRepository
    {
        Task<PagedResult<Post>> SearchPostAsync(string query, int page = 1);
    }
}