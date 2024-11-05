/// <summary>
/// Represents metadata for paging information.
/// </summary>
namespace Knoex.Data
{
    public class PagingMeta
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total number of rows.
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Gets the number of the first row on the current page.
        /// </summary>
        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

        /// <summary>
        /// Gets the number of the last row on the current page.
        /// </summary>
        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
    }

    /// <summary>
    /// Represents a paged result set with metadata.
    /// </summary>
    /// <typeparam name="T">The type of the items in the result set.</typeparam>
    public class PagedResult<T> where T : class
    {
        /// <summary>
        /// Gets or sets the list of results.
        /// </summary>
        public IList<T> Results { get; set; }

        /// <summary>
        /// Gets or sets the paging metadata.
        /// </summary>
        public PagingMeta Meta { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
        /// </summary>
        public PagedResult()
        {
            Results = new List<T>();
            Meta = new PagingMeta();
        }
    }
}