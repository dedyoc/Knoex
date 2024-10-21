using Microsoft.AspNetCore.Mvc;

namespace Knoex.Controllers
{
    public abstract class CommonController : Controller
    {
        private readonly List<string> _notifications = new();
        protected void AddNotification(string title, string message, bool success = true)
        {
            string type = success ? "success" : "failure";
            _notifications.Add($"{type}||{title}||{message}");
            TempData["Notifications"] = _notifications;
        }
    }

}