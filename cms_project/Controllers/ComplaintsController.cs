using System.Security.Claims;
using cms_project.Data;
using cms_project.Models.Entites;
using cms_project.Models.ViewModel;
using cms_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace cms_project.Controllers
{
     
    public class ComplaintsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment environment;
        public ComplaintsController(ApplicationDbContext dbContext , IWebHostEnvironment environment) 
        { 
        this.dbContext = dbContext;
        this.environment = environment;
        }
        [HttpGet]
        public IActionResult Create()
        {

            var complaintTypes= dbContext.ComplaintTypes.Select(x=>new ComplaintTypesViewModel
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
                var complaintTypes = dbContext.ComplaintTypes.Select(x => new ComplaintTypesViewModel
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
            
          

           

            dbContext.Set<Complaint>().Add(complaint);
            await dbContext.SaveChangesAsync();
          var x =  new EmailService();
            x.Send(userEmail,"Complaint Submited",$"Dear {User.FindFirstValue("Name")}, Thank you for Submited The Complaint" );
            return RedirectToAction("Create","Complaints");
        }


    }
}
