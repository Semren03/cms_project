using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class AttachmentComplaint
{
    [Key]
    public Guid Id { get; set; }

    public string AttachmentName { get; set; }

   
    public Guid ComplaintId { get; set; }

   
    
    public Complaint Complaint { get; set; }
}
