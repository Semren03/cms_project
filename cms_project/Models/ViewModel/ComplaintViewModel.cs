using System.ComponentModel.DataAnnotations;


namespace cms_project.Models.ViewModel
{
    public class ComplaintViewModel
    {
        [Required(ErrorMessage = "Complaint type is required")]
        [Display(Name = "Complaint Type")]
        public int ComplaintTypeId { get; set; }

        public List<ComplaintTypesViewModel>? ComplaintTypeList { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title must not exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public List<IFormFile> Attachments { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
