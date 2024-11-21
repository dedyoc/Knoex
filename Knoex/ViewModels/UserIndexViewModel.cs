using Knoex.Data;
using Knoex.Models;

namespace Knoex.ViewModels
{
    public class UserIndexViewModel : BaseViewModel
    {
        public PagedResult<User> Users { get; set; }
    }
}