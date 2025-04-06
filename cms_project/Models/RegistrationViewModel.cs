using System.ComponentModel.DataAnnotations;

namespace cms_project.Models
{
    public class RegistrationViewModel
    {
        [Key]
        [Required(ErrorMessage = "StudendID is required.")]
        [MaxLength(10, ErrorMessage = "Max 10 Characters allowed.")]
        [MinLength(10, ErrorMessage = "Min 10 Characters allowed.")]
        public string StudentID { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(30, ErrorMessage = "Max 30 Characters allowed.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(30, ErrorMessage = "Max 30 Characters allowed.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 Characters allowed.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Max 20 Characters allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]   
        public string ConfirmPassword { get; set; }


    }
}
