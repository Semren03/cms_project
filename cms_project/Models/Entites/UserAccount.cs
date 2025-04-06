using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cms_project.Models.Entites
{
    [Index(nameof(Email),IsUnique =true)]
    [Index(nameof(StudentID), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        [Required(ErrorMessage = "StudendID is required.")]
        [MaxLength(10, ErrorMessage = "Max 10 Characters allowed.")]
        [MinLength(10, ErrorMessage = "Min 10 Characters allowed.")]
        public string StudentID { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(30,ErrorMessage ="Max 30 Characters allowed.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(30, ErrorMessage = "Max 30 Characters allowed.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(50, ErrorMessage = "Max 50 Characters allowed.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MaxLength(30, ErrorMessage = "Max 20 Characters allowed.")]
        public string Password { get; set; }


    }
}
