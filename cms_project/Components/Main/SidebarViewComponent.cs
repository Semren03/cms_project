using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

public class SidebarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var roleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Role");
        var user = HttpContext.User;
        var permissions = user.FindAll("Permission").Select(c => c.Value).ToList();
        var role = user.FindFirst("Role")?.Value;
        var claims = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Claims");
        return View(new SideBarViewModel { Role = role ,Permission=permissions});
    }
}
