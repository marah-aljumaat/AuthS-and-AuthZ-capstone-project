using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace Authentication_and_authorization_capstone_project.Models
{
  public class ApplicationUser : IdentityUser
  {
      [Required]
      [StringLength(50)]
      public string FirstName { get; set; } = string.Empty;

      [Required]
      [StringLength(50)]
      public string LastName { get; set; } = string.Empty;
  } 
}

