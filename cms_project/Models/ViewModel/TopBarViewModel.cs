using cms_project.Models.Entites;

namespace cms_project.Models.ViewModel
{
    public class TopBarViewModel
    {
        public string Username { get; set; }
        public List<Notification> Notifications { get; set; } = new List<Notification>();

    }
}
