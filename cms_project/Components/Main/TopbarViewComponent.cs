using cms_project.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace cms_project.Componant.Main
{
    public class TopbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var username = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Name").Value;
            return View(new TopBarViewModel { Username= username  });
        }
    }
}
