using System.ComponentModel.DataAnnotations;

namespace cms_project.Models.Entites
{
    public class ComplaintType
    {
 
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();

        public ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
    }

}
