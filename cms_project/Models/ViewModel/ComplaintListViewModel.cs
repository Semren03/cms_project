namespace cms_project.Models.ViewModel
{
    public class ComplaintListViewModel
    {
        public string ComplaintType { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public List<string> Attachment { get; set; }
    }
}
