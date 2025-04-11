using System.ComponentModel.DataAnnotations;

namespace cms_project.Models.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
