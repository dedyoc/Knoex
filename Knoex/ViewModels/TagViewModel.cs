using Knoex.Data;
using Knoex.Models;

namespace Knoex.ViewModels
{
    public class TagViewModel : BaseViewModel
    {
        public PagedResult<Tag> Tags { get; set; }
    }
}