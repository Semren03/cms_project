namespace cms_project.Models.Entites
{
    public class Complaint
    {
        public int Id { get; set; }

        public  string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? StudentName { get; set; }
        public List<AttachmentComplaint>? AttachmentComplaints { get; set; } 

    }
}
