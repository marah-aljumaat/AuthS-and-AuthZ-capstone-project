using System.ComponentModel.DataAnnotations;
namespace Authentication_and_authorization_capstone_project.Models
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
