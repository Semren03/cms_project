using cms_project.Models.Entites;

namespace cms_project.Models.ViewModel
{
    public class AddUserRoleViewModel
    {
        public List<Role>  Roles { get; set; }
        public int UserId { get; set; } 
        public int RoleId { get; set; } 
    }
}
