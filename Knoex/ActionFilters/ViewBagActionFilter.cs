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
        /// Called before the action result executes. Populates the ViewBag with the current user information if the user is authenticated.
        /// </summary>
        /// <param name="context">The result executing context.</param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is Controller)
            {
                var controller = context.Controller as Controller;
                controller.ViewBag.CurrentUser = context.HttpContext.User.Identity.IsAuthenticated
                    ? _userRepository.GetCurrentUser() : null;
            }
            base.OnResultExecuting(context);
        }
    }
}