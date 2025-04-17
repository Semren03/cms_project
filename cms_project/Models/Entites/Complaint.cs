using cms_project.Models.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

public enum ComplaintType
{
    Academic,
    Administrative,
    Infrastructure
}

public class Complaint
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Complaint type is required")]
    public ComplaintType ComplaintType { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title must not exceed 100 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

   
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    

    [Required(ErrorMessage = "Student name is required")]
    [StringLength(100, ErrorMessage = "Student name must not exceed 100 characters")]

    [ForeignKey(nameof(UserAccount))]
    public int CreatedBy { get; set; }

     
    public UserAccount UserAccount { get; set; }

    public List<AttachmentComplaint> AttachmentComplaints { get; set; } =new List<AttachmentComplaint>();
}
