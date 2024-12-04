using System.ComponentModel.DataAnnotations;

namespace Asseti_Fi.Models;

public class LoginUser
{
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public required string Password { get; set; }
}