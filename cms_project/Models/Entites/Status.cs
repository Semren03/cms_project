namespace cms_project.Models.Entites
{
    public class Status
    {
        public int StatusId { get; set; }

        public string StatusName { get; set; }
        
        public ICollection<Complaint> Complaints { get; set; }
    }
}
