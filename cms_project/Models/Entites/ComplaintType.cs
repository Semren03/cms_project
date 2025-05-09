using System.ComponentModel.DataAnnotations;

namespace cms_project.Models.Entites
{
    public class ComplaintType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
    }

}
