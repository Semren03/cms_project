using System.ComponentModel.DataAnnotations;


namespace cms_project.Models.ViewModel
{
    public class ComplaintViewModel
    {
        
        public int ComplaintTypeId { get; set; }

        public List<ComplaintTypesViewModel>? ComplaintTypeList { get; set; }

       
        public string Title { get; set; }

       
        public string Description { get; set; }

        public List<IFormFile>? Attachments { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
