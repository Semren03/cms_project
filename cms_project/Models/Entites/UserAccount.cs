using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace cms_project.Models.Entites
{
    public class UserAccount
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [ForeignKey("ComplaintTypeResolverId")]
        public ComplaintType ComplaintType { get; set; }
        public int? ComplaintTypeResolverId { get; set; }



        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Complaint>? AssignedUsers { get; set; }







    }
}
