using System.Security.Claims;
using cms_project.Data;
using cms_project.Models.Entites;
using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace cms_project.Controllers
{
    public class MangementAdminController : Controller
    {
        private readonly ApplicationDbContext context;

        public MangementAdminController(ApplicationDbContext context)
        {
            this.context = context;
        }

    

        [HttpGet]
        public IActionResult Create()
        {
            var claims = context.Claims.ToList();

            ViewBag.Claims = new SelectList(claims, "Id", "Name");

            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Manage Roles")]
        public IActionResult ManageRoles()
        {
            var roles = context.Roles.Include(r => r.Claims).ToList();


            ViewBag.Roles = roles;

            return View(roles);
        }

        [HttpPost]
        [Authorize(Policy = "Manage Roles")]
        public IActionResult Create(Role role, List<int> selectedClaims)
        {
            if (ModelState.IsValid)
            {
                var claims = context.Claims.Where(c => selectedClaims.Contains(c.Id))
                                                      .ToList();
                foreach (var claim in claims)
                {
                    role.Claims.Add(claim);
                }
                context.Roles.Add(role);
                context.SaveChanges();

                return RedirectToAction("ManageRoles");
            }

            var allClaims = context.Claims.ToList();
            ViewBag.Claims = new SelectList(allClaims, "Id", "Name");

            return View(role);
        }

        [HttpGet]

        [Authorize(Policy = "Manage Roles")]

        public IActionResult EditRole(int id)
        {
            var role = context.Set<Role>().Include(x=>x.Claims).FirstOrDefault(x => x.Id == id);
            var claims = context.Claims.ToList();

            ViewBag.Claims = new SelectList(claims, "Id", "Name");
            return View(role);
        }
        [HttpPost]
        [Authorize(Policy = "Manage Roles")]
        public IActionResult UpdateRole(Role role, List<int> selectedClaims)
        {
            var roleExist = context.Roles
                .Include(r => r.Claims)
                .FirstOrDefault(r => r.Id == role.Id);

            if (roleExist == null)
                return NotFound();

            // Update scalar properties
            roleExist.Name = role.Name;

            var claimsToAdd = context.Claims
                .Where(c => selectedClaims.Contains(c.Id))
                .ToList();

            var existingClaimIds = roleExist.Claims.Select(c => c.Id).ToList();

            // Add new claims
            foreach (var claim in claimsToAdd)
            {
                if (!existingClaimIds.Contains(claim.Id))
                {
                    roleExist.Claims.Add(claim);
                }
            }

            // Optional: remove unchecked claims
            var claimsToRemove = roleExist.Claims
                .Where(c => !selectedClaims.Contains(c.Id))
                .ToList();

            foreach (var claim in claimsToRemove)
            {
                roleExist.Claims.Remove(claim);
            }

            context.SaveChanges();
            return RedirectToAction("ManageRoles");
        }


        [HttpGet]
        public IActionResult AddUser()
        {
            var roles = context.Set<Role>().Include(x=>x.Claims).AsNoTracking().ToList();
            var categories = context.Set<ComplaintType>().AsNoTracking().ToList();  
            return View(new AddUserRoleViewModel { Roles =roles,ComplaintTypes=categories});
        }

        [HttpPost]
        public IActionResult AddUser(AddUserRoleViewModel model)
        {
            var user = context.Set<UserAccount>()
                               .FirstOrDefault(u => u.Id ==model.UserId);
            user.RoleId = model.RoleId;
            if (model.ComplaintTypeResolverId != null && model.ComplaintTypeResolverId > 0)
            {
                user.ComplaintTypeResolverId = model.ComplaintTypeResolverId;
            }
            else
                user.ComplaintTypeResolverId = null;

                context.Update(user);
            context.SaveChanges();
            return Redirect("MangementTableUser");
        }
        [HttpGet]
        public IActionResult GetUserData(string Email)
        {
            var user = context.Set<UserAccount>()
                              .FirstOrDefault(u => u.Email.ToLower()== Email.ToLower());

            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            return Json(new { success = true, user = new {user.Id, user.Name, user.Email ,user.RoleId,user.ComplaintTypeResolverId} });
        }





        [HttpGet]
        public IActionResult MangementTableUser()
        {
            var UserWITHRoles = context.Set<UserAccount>().Include(x => x.Role).ToList();
            return View(UserWITHRoles);
        }

        [HttpGet]

        public IActionResult MangementTableComplaintType()
        {
            var UserWITHComplaintType = context.Set<UserAccount>()
                .Include(x => x.Role)
                .Include(x => x.ComplaintType)
                .Where(x => x.Role.Name == "Complaint Handler")
                .ToList();
            return View(UserWITHComplaintType);
        }

        [HttpGet]

        [HttpGet]
        public IActionResult EditComplaintType(int id)
        {
            var user = context.Set<UserAccount>()
                .Include(x => x.ComplaintType)
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            ViewBag.ComplaintTypes = context.ComplaintTypes.ToList();     
            return View(user);
        }

        [HttpPost]
        public IActionResult EditComplaintType(int id, int? ComplaintTypeResolverId)
        {
            var user = context.Set<UserAccount>().FirstOrDefault(x => x.Id == id);

            if (user == null)
                return NotFound();

            user.ComplaintTypeResolverId = ComplaintTypeResolverId;
            context.SaveChanges();

            return RedirectToAction("MangementTableComplaintType");
        }
        [Authorize(Policy = "Email Setting")]
        public async Task<IActionResult> EditEmailSetting()
        {
            var settings = await context.EmailSettings.FirstOrDefaultAsync(x=>x.Id ==1);
            if (settings == null)
                return NotFound();

            return View(settings);
        }

        [HttpPost]
        [Authorize(Policy = "Email Setting")]
        public async Task<IActionResult> EditEmailSetting( EmailSettings model)
        {
            if (ModelState.IsValid)
            {
                    context.Update(model);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index","Dashboard");
            }

            return View(model);
        }



    }
}
