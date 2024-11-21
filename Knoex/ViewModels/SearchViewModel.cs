using Knoex.Data;
using Knoex.Models;

namespace Knoex.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public string Query { get; set; }
        public PagedResult<Post> Posts { get; set; }
    }
}