using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

public class SidebarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var roleClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Role");
        var role = roleClaim?.Value ?? "";
        return View(new SideBarViewModel { Role = role });
    }
}
