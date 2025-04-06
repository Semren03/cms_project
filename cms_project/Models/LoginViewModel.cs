using System.ComponentModel.DataAnnotations;

namespace cms_project.Models
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(10, ErrorMessage = "Max 10 Characters allowed.")]
        [Required(ErrorMessage ="StudentID is required.")]
        [MinLength(10, ErrorMessage = "Min 10 Characters allowed.")]
        public string StudentID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 Characters allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
