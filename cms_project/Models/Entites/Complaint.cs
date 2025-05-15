using cms_project.Models.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;



public class Complaint
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int ComplaintTypeId { get; set; }

    [ForeignKey(nameof(ComplaintTypeId))]
    public ComplaintType ComplaintType { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title must not exceed 100 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

   
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    

    [Required(ErrorMessage = "Student name is required")]
    [StringLength(100, ErrorMessage = "Student name must not exceed 100 characters")]

    
    public int CreatedBy { get; set; }
    public UserAccount UserAccount { get; set; }



    
    public int? AssignedTo { get; set; }
    public UserAccount AssignedUser { get; set; }

    public int StatusId { get; set; }

    [ForeignKey(nameof(StatusId))]
    public Status Status { get; set; }

    public List<AttachmentComplaint> AttachmentComplaints { get; set; } =new List<AttachmentComplaint>();
    public List<ComplaintHistory> ComplaintHistories { get; set; } = new List<ComplaintHistory>();
}
   public enum ComplaintStatus
{
    Pending = 1,
    InProgress = 3,
    Resolved = 4 ,
    Closed = 5,
    Rejected = 6,
    ReOpen = 1002


}