using System.ComponentModel.DataAnnotations;
namespace Authentication_and_authorization_capstone_project.Models
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@".*\S.*", ErrorMessage = "First name cannot be blank.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@".*\S.*", ErrorMessage = "Last name cannot be blank.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(128, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
