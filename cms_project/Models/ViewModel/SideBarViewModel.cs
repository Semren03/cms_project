using cms_project.Models.Entites;

namespace cms_project.Models.ViewModel
{
    public class SideBarViewModel
    {
        public List<AttachmentComplaint> AttachmentComplaints { get; set; }
        public List<Complaint> Complaints { get; set; }
        public string Role { get; set; }
    }
}
