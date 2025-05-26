using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using cms_project.Models.Entites;
using cms_project.Data;

namespace cms_project.Componant.Main
{
    public class TopbarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;

        public TopbarViewComponent(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IViewComponentResult Invoke()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Name")?.Value ?? "Guest";
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            List<Notification> notifications = new List<Notification>();

            if (int.TryParse(userIdClaim, out int userId))
            {
                notifications = context.Set<Notification>()
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.CreatedDate)
                    .Take(5)
                    .ToList();
            }

            var model = new TopBarViewModel
            {
                Username = username,
                Notifications = notifications
            };

            return View(model);
        }
    }
}
