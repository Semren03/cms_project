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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    

                    var user = await dbContext.Set<UserAccount>()
                                             .Include(x => x.Role)
                                             .ThenInclude(x => x.Claims)
                                             .Where(x => x.Email == model.Email && x.Password == model.Password)
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync();

                    if (user != null)
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("Name", user.Name),
                    new Claim("Role", user.Role.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                      
                        foreach (var claim in user.Role.Claims)
                        {
                            claims.Add(new Claim("Permission", claim.Name));
                        }

                        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                        await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                       
                        ModelState.AddModelError("", "Invalid email or password. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    

                    ModelState.AddModelError("", "An error occurred during login. Please try again.");
                }
            }

            return View(model);
        }
        [Authorize]    
        [HttpPost]
            public IActionResult Logout()
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }

 
    }
}
