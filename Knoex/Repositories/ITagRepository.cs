using Knoex.Models;

namespace Knoex.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> GetOrCreateTagAsync(string tagName);
    }
}