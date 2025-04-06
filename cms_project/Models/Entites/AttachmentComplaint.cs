namespace cms_project.Models.Entites
{
    public class AttachmentComplaint
    {
        public int Id { get; set; }
        public string Path {  get; set; }   





        public int ComplaintId { get; set; }
        public Complaint? Complaint { get; set; }
    }
}
