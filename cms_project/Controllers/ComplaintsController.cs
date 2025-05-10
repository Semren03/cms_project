using System.Security.Claims;
using cms_project.Data;
using cms_project.Models.Entites;
using cms_project.Models.ViewModel;
using cms_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace cms_project.Controllers
{
     
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;
        public ComplaintsController(ApplicationDbContext dbContext , IWebHostEnvironment environment) 
        { 
        this.context = dbContext;
        this.environment = environment;
        }
        [HttpGet]
        public IActionResult Create()
        {

            var complaintTypes= context.ComplaintTypes.Select(x=>new ComplaintTypesViewModel
            {
               Id  = x.Id,
               Name =x.Name,
            }).ToList();
            return View(new ComplaintViewModel() { ComplaintTypeList=complaintTypes});
        }
        [HttpPost]
        public async Task<IActionResult> Create(ComplaintViewModel cvm)
        {


            if (!ModelState.IsValid) 
            {
                var complaintTypes = context.ComplaintTypes.Select(x => new ComplaintTypesViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                return View(cvm.ComplaintTypeList=complaintTypes);
            }
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Cannot Send The Request");
            }

            var userEmail = User.FindFirstValue(ClaimTypes.Email);


            Complaint complaint = new Complaint()
                {
                ComplaintTypeId = cvm.ComplaintTypeId,
                    Title = cvm.Title,
                    Description = cvm.Description,
                    CreatedBy = userId  ,
                    StatusId = 1
                    
            };
            
            if (cvm.Attachments.Count > 0)
            {
                foreach (var file in cvm.Attachments)
                {
                    if (file.Length > 0)
                    {
                        var uploadFolder = Path.Combine("wwwroot", "attachment");
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }
                        var fileName = Path.GetFileName(file.FileName); 
                        complaint.AttachmentComplaints.Add(new AttachmentComplaint { AttachmentName =fileName});


                        var fullPath = Path.Combine(uploadFolder, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        }
                }
            }
            
          

           

            context.Set<Complaint>().Add(complaint);
            await context.SaveChangesAsync();
          var x =  new EmailService();
            x.Send(userEmail,"Complaint Submited",$"Dear {User.FindFirstValue("Name")}, Thank you for Submited The Complaint" );
            return RedirectToAction("Create","Complaints");
        }

        [HttpGet]
        public IActionResult ShowComplaint()
        {

            var userComplaint = context.Set<Complaint>().Include(x => x.UserAccount).Include(x => x.AttachmentComplaints)
                                                     .Include(x => x.ComplaintType)
                .Select(x => new ComplaintListViewModel
                {
                    Id = x.Id,
                    ComplaintType = x.ComplaintType.Name,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    Title = x.Title,
                    Username = x.UserAccount.Name,
                    Status = x.Status.StatusName,
                    Attachment = x.AttachmentComplaints.Select(x => x.AttachmentName).ToList()
                })
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

            var userComplaint = context.Set<Complaint>()
                .Where(x => x.CreatedBy == userId)
                .Include(x => x.UserAccount)
                .Include(x => x.AttachmentComplaints)
                .Include(x => x.ComplaintType)
                .Select(x => new ComplaintListViewModel
                {
                    ComplaintType = x.ComplaintType.Name,
                    CreatedDate = x.CreatedDate,
                    Description = x.Description,
                    Title = x.Title,
                    Attachment = x.AttachmentComplaints
                                 .Select(x => x.AttachmentName).ToList()

                })
                .ToList();

            return View(userComplaint);
        }

        public async Task<IActionResult> ComplaintDetails(Guid id)
        {


            var complaint = await context.Set<Complaint>()
              .Where(c => c.Id.Equals(id))
              .Select(x => new ComplaintListViewModel
              {
                  Id = x.Id,
                  ComplaintType = x.ComplaintType.Name,
                  ComplaintTypeId = x.ComplaintType.Id,
                  CreatedDate = x.CreatedDate,
                  Description = x.Description,
                  Title = x.Title,
                  Username = x.UserAccount.Name,
                  Status = x.Status.StatusName,
                  Attachment = x.AttachmentComplaints.Select(a => a.AttachmentName).ToList()
              })
               .FirstOrDefaultAsync();

            var users = await context.Set<UserAccount>().Where(x=>x.ComplaintTypeResolverId ==complaint.ComplaintTypeId).ToListAsync();
            ViewBag.Users = new SelectList(users, "Id", "Name");


            return View(complaint);
        }

        public async Task<IActionResult> InboxComplaint()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Cannot Get User Information");
            }

            var userComplaint = context.Set<Complaint>()
                .Where(x => x.AssignedTo == userId)
                .Include(x => x.UserAccount)
                .Include(x => x.AttachmentComplaints)
                .Include(x => x.ComplaintType)
                .Select(x => new ComplaintListViewModel
                {
                    Id=x.Id,
                    ComplaintType = x.ComplaintType.Name,
                    CreatedDate = x.CreatedDate,
                    Title = x.Title,
                    Status=x.Status.StatusName,
                    Username =x.UserAccount.Name,
               

                })
                .ToList();

            return View(userComplaint);
        }

        [HttpPost]
        public IActionResult AssignTo(Guid complaintId, int userId)
        {
            var complaint = context.Set<Complaint>().FirstOrDefault(x => x.Id.Equals(complaintId));

            complaint.AssignedTo = userId;
            complaint.StatusId = 3; 
            context.Update(complaint);
            context.SaveChanges();


            return RedirectToAction("Complaints");

        }


    }
}
