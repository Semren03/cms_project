using cms_project.Data;
using cms_project.Models.Entites;
using Microsoft.AspNetCore.Mvc;

namespace cms_project.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ComplaintsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Complaint model)
        {
            model.CreatedDate = DateTime.UtcNow;
           
            _context.Add(model);
            _context.SaveChanges();
           return RedirectToAction("index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
