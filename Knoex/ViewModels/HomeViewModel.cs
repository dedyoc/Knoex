using Knoex.Data;
using Knoex.Models;

namespace Knoex.ViewModels
{
    /// <summary>
    /// Represents the view model for the Home view.
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the list of available filters for posts.
        /// Each filter is represented as a key-value pair where the key is the filter identifier and the value is the display name.
        /// </summary>
        public List<KeyValuePair<string, string>> Filters { get; set; } = new()
        {
            new KeyValuePair<string, string>("newest","Newest"),
            new KeyValuePair<string, string>("unanswered", "Unanswered"),
            new KeyValuePair<string, string>("active", "Recent Activity")
        };

        /// <summary>
        /// Gets or sets the current filter applied to the posts.
        /// </summary>
        public string CurrentFilter { get; set; }

        /// <summary>
        /// Gets or sets the paginated result of posts.
        /// </summary>
        public PagedResult<Post> Posts { get; set; }
    }
}