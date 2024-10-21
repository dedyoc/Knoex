using Knoex.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Knoex.ActionFilter
{
    public class ViewBagActionFilter : ActionFilterAttribute
    {
        private readonly IUserRepository _userRepository;
        public ViewBagActionFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Called before the action result executes.
        /// </summary>
        /// <param name="context">The context in which the result is executing.</param>
        /// <remarks>
        /// This method checks if the controller is of type <see cref="Controller"/>. 
        /// If so, it sets the <see cref="ViewBag.CurrentUser"/> property to the current user if authenticated, 
        /// otherwise sets it to null. It also sets the <see cref="ViewBag.Notifications"/> property to the 
        /// notifications stored in TempData or initializes it to an empty list if no notifications are present.
        /// </remarks>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is Controller)
            {
                var controller = context.Controller as Controller;
                controller.ViewBag.CurrentUser = context.HttpContext.User.Identity.IsAuthenticated
                    ? _userRepository.GetCurrentUser() : null;
                controller.ViewBag.Notifications = controller.TempData["Notifications"] ?? new List<string>();
            }
            base.OnResultExecuting(context);
        }
    }
}