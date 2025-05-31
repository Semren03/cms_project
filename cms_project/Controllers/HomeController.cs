using Microsoft.AspNetCore.Mvc;

namespace cms_project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Page");
        }
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
