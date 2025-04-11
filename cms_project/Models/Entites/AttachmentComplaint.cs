using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class AttachmentComplaint
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Attachment name is required")]
    [StringLength(255)]
    public string AttachmentName { get; set; }

   
    public Guid ComplaintId { get; set; }

   
    
    public Complaint Complaint { get; set; }
}
