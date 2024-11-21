using Knoex.Data;
using Knoex.Models;

namespace Knoex.Repositories
{
    public interface ITagRepository
    {
        Task<PagedResult<Tag>> GetTagsAsync(int page = 1);
        Task<Tag> GetOrCreateTagAsync(string tagName);
    }
}