using Microsoft.EntityFrameworkCore;

namespace Knoex.Data
{
    public static class PagingExtension
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                Meta = new PagingMeta
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    RowCount = query.Count()
                }
            };
            var pageCount = (double)result.Meta.RowCount / pageSize;
            result.Meta.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();
            return result;
        }
    }
}