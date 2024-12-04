using System.ComponentModel.DataAnnotations;
namespace Asseti_Fi.Models;

public class RegisterUser
{
    
        [Required(ErrorMessage = "User type is required.")]
        public required string UserType { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public required string Password { get; set; }

        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
    
}