using System.Security.Claims;
using cms_project.Data;
using cms_project.Models;
using cms_project.Models.Entites;
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
            return View(dbContext.UserAccounts.ToList());
        }
 
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.StudentID = model.StudentID;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Email = model.Email;
                account.Password = model.Password;
                try
                {

                    dbContext.UserAccounts.Add(account);
                    dbContext.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please login.";
                }
                catch (DbUpdateException ex)
                {

                    ModelState.AddModelError("", "Please Enter UNIQUE Email or Password");
                    return View(model);
                }
                return View();
            }

            
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
                var user = dbContext.UserAccounts.Where(x => x.StudentID == model.StudentID  && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    //Success , create cookie done
                    var claims = new List<Claim>
                    {
                        new Claim (ClaimTypes.Name,user.FirstName),
                        new Claim ("Name",user.FirstName),
                        new Claim (ClaimTypes.Role,"User"),

                    };
                    var claimsIdentity = new ClaimsIdentity (claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "StudentID or Password is not Correct");
                }
            }
                return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name; 
            return View();
        }
    }
}
