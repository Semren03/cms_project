using cms_project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cms_project.Controllers
{
    

    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext context;
        

        public DashboardController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int pendingCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Pending);
            int inProgressCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.InProgress);
            int resolvedCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Resolved);
            int CompletedCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Closed);

            ViewBag.PendingCount = pendingCount;
            ViewBag.InProgressCount = inProgressCount;
            ViewBag.ResolvedCount = resolvedCount;
            ViewBag.CompletedCount = CompletedCount;

            return View();
        }



    }
}
