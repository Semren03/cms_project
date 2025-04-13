using System.Security.Claims;
using cms_project.Data;
using cms_project.Models.Entites;
using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ComplaintViewModel cvm)
        {
           
            if (!ModelState.IsValid) 
            {
                return View(cvm);
            }
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Cannot Send The Request");
            }

            Complaint complaint = new Complaint()
                {
                    ComplaintType = cvm.ComplaintType,
                    Title = cvm.Title,
                    Description = cvm.Description,
                    StudentName = cvm.StudentName,
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
            
          

           

            dbContext.Complaints.Add(complaint);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Create","Complaints");
        }


    }
}
