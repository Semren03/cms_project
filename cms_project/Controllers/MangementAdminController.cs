using cms_project.Data;
using cms_project.Models.Entites;
using cms_project.Models.ViewModel;
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
        public IActionResult ManageRoles()
        {
            var roles = context.Roles.Include(r => r.Claims).ToList();


            ViewBag.Roles = roles;

            return View(roles);
        }

        [HttpPost]
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
        public IActionResult EditRole(int id)
        {
            var role = context.Set<Role>().Include(x=>x.Claims).FirstOrDefault(x => x.Id == id);
            var claims = context.Claims.ToList();

            ViewBag.Claims = new SelectList(claims, "Id", "Name");
            return View(role);
        }
        [HttpPost]
        public IActionResult UpdateRole(Role role, List<int> selectedClaims)
        {
            if (ModelState.IsValid)
            {
                var claims = context.Claims.Where(c => selectedClaims.Contains(c.Id))
                    .ToList();
                foreach (var claim in claims)
                {
                    role.Claims.Add(claim);
                }
                context.Roles.Update(role);
                context.SaveChanges();

                return RedirectToAction("ManageRoles");
            }

            var allClaims = context.Claims.ToList();
            ViewBag.Claims = new SelectList(allClaims, "Id", "Name");

            return View(role);
        }


        [HttpGet]
        public IActionResult AddUser()
        {
            var roles = context.Set<Role>().AsNoTracking().ToList();
            return View(new AddUserRoleViewModel { Roles =roles});
        }

        [HttpPost]
        public IActionResult AddUser(AddUserRoleViewModel model)
        {
            var user = context.Set<UserAccount>()
                               .FirstOrDefault(u => u.Id ==model.UserId);
            user.RoleId = model.RoleId; 
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

            return Json(new { success = true, user = new {user.Id, user.Name, user.Email ,user.RoleId} });
        }





        [HttpGet]
        public IActionResult MangementTableUser()
        {
            var UserWITHRoles = context.Set<UserAccount>().Include(x => x.Role).ToList();
            return View(UserWITHRoles);
        }


    }
}
