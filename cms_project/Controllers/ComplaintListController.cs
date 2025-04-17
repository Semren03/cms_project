using System.Security.Claims;
using cms_project.Data;
using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace cms_project.Controllers
{
    public class ComplaintListController  : Controller
    {
        private readonly ApplicationDbContext context;
      public ComplaintListController(ApplicationDbContext context)
        {
             this.context = context;
        }

        [HttpGet]
      public IActionResult ShowComplaint()
        {
            
            

            var userComplaint = context.Complaints.Include(x=>x.UserAccount).Include(x=>x.AttachmentComplaints)
                .Select (x=>new ComplaintListViewModel
                {
                       ComplaintType =x.ComplaintType.ToString()    ,
                       CreatedDate =x.CreatedDate,
                       Description =x.Description,
                       Title =x.Title,
                       Username= x.UserAccount.Name  ,
                       Attachment =x.AttachmentComplaints.Select(x=>  x.AttachmentName).ToList()
                } )
                .ToList();
            return View(userComplaint);
        }
        [HttpGet]
        public IActionResult ShowHistoryComplaint()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Cannot Get User Information");
            }

            var userComplaint = context.Complaints
                .Where(x => x.CreatedBy == userId)
                .Include(x => x.UserAccount)
                .Include(x => x.AttachmentComplaints)
                .Select(x => new ComplaintListViewModel
                {
                    ComplaintType = x.ComplaintType.ToString(),
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    Title = x.Title,
                    Attachment = x.AttachmentComplaints
                                 .Select(x => x.AttachmentName).ToList()
                                 
                })
                .ToList();

            return View(userComplaint);
        }


    }

}

