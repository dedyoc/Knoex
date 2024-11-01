using Knoex.Data;
using Knoex.Models;
using Microsoft.EntityFrameworkCore;

namespace Knoex.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly KnoexContext _context;
        public TagRepository(KnoexContext context)
        {
            _context = context;
        }
        public async Task<Tag> GetOrCreateTagAsync(string tagName)
        {
            var tag = await _context.Tags.SingleOrDefaultAsync(t => t.Name == tagName);
            if (tag == null)
            {
                tag = new Tag { Name = tagName };
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
            }
            return tag;
        }
    }
}