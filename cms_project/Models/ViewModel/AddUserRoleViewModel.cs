using cms_project.Models.Entites;

namespace cms_project.Models.ViewModel
{
    public class AddUserRoleViewModel
    {
        public List<Role>  Roles { get; set; }
        public List<ComplaintType>  ComplaintTypes { get; set; }

        public int? ComplaintTypeResolverId { get; set; }
        public int UserId { get; set; } 
        public int RoleId { get; set; } 
    }
}
