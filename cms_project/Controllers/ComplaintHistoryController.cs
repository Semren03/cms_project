using System.Security.Claims;
using cms_project.Data;
using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace cms_project.Controllers
{
    public class ComplaintHistoryController  :Controller
    {
        private readonly ApplicationDbContext context;

        public ComplaintHistoryController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult ComplaintHistoryTabel()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!int.TryParse(userIdClaim,out int userId))
            {
                return BadRequest("Cannot Get User Information");
            }

            var userComplaint = context.Complaints.Where(x => x.CreatedBy == userId).ToList();

            var viewModel = new SideBarViewModel
            {
                Complaints = userComplaint,

            };

            return View(viewModel);

        }
    }
}
