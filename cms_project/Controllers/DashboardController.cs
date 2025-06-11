using System.Security.Claims;
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
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var user = await context.Set<UserAccount>()
                                    .Include(u => u.Role)
                                    .FirstOrDefaultAsync(u => u.Id == userId);

            string roleName = user?.Role?.Name;
            ViewBag.Role = roleName;

            if (roleName == "SuperAdmin")
            {
                ViewBag.PendingCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Pending);
                ViewBag.InProgressCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.InProgress);
                ViewBag.ResolvedCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Resolved);
                ViewBag.CompletedCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Closed);
            }
            else
            {
                ViewBag.PendingCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Pending && c.CreatedBy == userId);
                ViewBag.InProgressCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.InProgress && c.CreatedBy == userId);
                ViewBag.ResolvedCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Resolved && c.CreatedBy == userId);
                ViewBag.CompletedCount = await context.Set<Complaint>().CountAsync(c => c.StatusId == (int)ComplaintStatus.Closed && c.CreatedBy == userId);
            }


      

            var announcements = await context.Announcements
                .Where(a => a.IsActive)
                .OrderByDescending(a => a.DatePosted)
                .ToListAsync();

            var threeDaysAgo = DateTime.Now.AddDays(-3);

            ViewBag.OverdueCount = await context.Set<Complaint>()
                .Where(c => c.StatusId != (int)ComplaintStatus.Resolved && c.StatusId != (int)ComplaintStatus.Closed)
                .Where(c => c.CreatedDate < threeDaysAgo)
                .CountAsync();

            var complaintStats = await context.Set<Complaint>()
                .GroupBy(c => c.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            ViewBag.ChartLabels = complaintStats.Select(s => s.Status).ToArray();
            ViewBag.ChartData = complaintStats.Select(s => s.Count).ToArray();

            return View(announcements);
        }

        public ActionResult FAQ()
        {
            return View();
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

       
        
        [HttpGet]
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
