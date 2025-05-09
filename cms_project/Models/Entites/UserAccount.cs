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

        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

        public ICollection<Complaint>? AssignedUsers { get; set; }







    }
}
