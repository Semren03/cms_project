namespace cms_project.Models.Entites
{
    public class ComplaintHistory
    {
        public int Id { get; set; }
        public string ActionStatus { get; set; }

        public Guid ComplaintId { get; set; }
        public Complaint Complaint { get; set; }

        public string ResolverName { get; set; }

        public string Comments { get; set; }

        public DateTime CreatedDate { get; set; }
        public ComplaintHistory() { }

        public ComplaintHistory(Guid complaintId, string actionStatus, string comments,string resolverName)
        {
            this.ComplaintId = complaintId;
            this.ResolverName = resolverName;
            this.ActionStatus = actionStatus;
            this.Comments = comments;   
            this.CreatedDate = DateTime.Now;
        }  
        


    }
}
