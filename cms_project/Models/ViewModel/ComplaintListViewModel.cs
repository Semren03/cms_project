using cms_project.Models.Entites;

namespace cms_project.Models.ViewModel
{
    public class ComplaintListViewModel
    {
        public Guid Id { get; set; }
        public string ComplaintType { get; set; }
        public int ComplaintTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public int AssignToId { get; set; }
        public string? AssigneeName { get; set; }
        public int CreatedBy { get; set; }
        public List<string> Attachment { get; set; }

        public List<ComplaintHistory> ComplaintHistories { get; set; } = new List<ComplaintHistory>();

    }
}
