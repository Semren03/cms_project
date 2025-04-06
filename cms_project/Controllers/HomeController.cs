using System.Diagnostics;
using cms_project.Data;
using cms_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace cms_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
