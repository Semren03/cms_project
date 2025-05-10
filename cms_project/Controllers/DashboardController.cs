using Microsoft.AspNetCore.Mvc;

namespace cms_project.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
