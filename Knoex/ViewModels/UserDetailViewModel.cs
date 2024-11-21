using Knoex.Data;
using Knoex.Models;

namespace Knoex.ViewModels
{
    public class UserDetailViewModel : BaseViewModel
    {
        public User User { get; set; }
        public PagedResult<Post> Posts { get; set; }
    }
}