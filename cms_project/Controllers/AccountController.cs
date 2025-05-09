using System.Security.Claims;
using cms_project.Data;
using cms_project.Models.Entites;
using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cms_project.Controllers
{
    public class AccountController : Controller
    {
       
        private readonly ApplicationDbContext dbContext;

        public AccountController(ApplicationDbContext appdbcontext)
        {
            dbContext = appdbcontext;
        }
        public IActionResult Index()
        {
            return View(dbContext.Set<UserAccount>().ToList());
        }
 
        public IActionResult Create()
        {
            return View();
        }


      
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = dbContext.Set<UserAccount>().Include(x=>x.Role)
                                                 .ThenInclude(x=>x.Claims)
                                                 .Where(x => x.Email == model.Email  && x.Password == model.Password)
                                                 .AsNoTracking()
                                                 .FirstOrDefault();
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim ("Name",user.Name),
                        new Claim("Role", user.Role.Name),
                        new Claim(ClaimTypes.Email,user.Email),
                    };
                    foreach(var claim in user.Role.Claims)
                    {
                        claims.Add(new Claim ("Permission", claim.Name));
                    }
                    var claimsIdentity = new ClaimsIdentity (claims, "CookieAuth");
                    HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "StudentID or Password is not Correct");
                }
                       
            }
                return View();
        }
        [HttpPost]
            public IActionResult Logout()
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
        [Authorize(Policy ="CanViewDashboard")]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Claims.FirstOrDefault(x=>x.Type =="Name").Value; 
            return View();
        }

 
    }
}
