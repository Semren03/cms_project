using cms_project.Data;
using cms_project.Models.Entites;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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

            var announcements = await context.Announcements
      .Where(a => a.IsActive)
      .OrderByDescending(a => a.DatePosted)
      .ToListAsync();

            var threeDaysAgo = DateTime.Now.AddDays(-3);

            var overdueCount = context.Set<Complaint>()
                .Where(c => c.StatusId != (int)ComplaintStatus.Resolved && c.StatusId != (int)ComplaintStatus.Closed)
                .Where(c => c.CreatedDate < threeDaysAgo)
                .Count();

            ViewBag.OverdueCount = overdueCount;

            var complaintStats = context.Set<Complaint>()
    .GroupBy(c => c.Status)
    .Select(g => new { Status = g.Key, Count = g.Count() })
    .ToList();

            ViewBag.ChartLabels = complaintStats.Select(s => s.Status).ToArray();
            ViewBag.ChartData = complaintStats.Select(s => s.Count).ToArray();


            return View(announcements);



        }


        [HttpGet]
        [Authorize(Policy = "Announcement")]


        public ActionResult CreateAnnouncement()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Announcement")]

        public ActionResult CreateAnnouncement(Announcement model)
        {
            if (ModelState.IsValid)
            {
                model.DatePosted = DateTime.Now;
                model.IsActive = true;

                context.Announcements.Add(model);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "Announcement")]

        public IActionResult DeleteAnnouncement(int id)
        {
            var announcement = context.Announcements.Find(id);
            if (announcement != null)
            {
                context.Announcements.Remove(announcement);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }







    }
}
