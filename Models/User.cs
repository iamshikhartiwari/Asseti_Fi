using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Asseti_Fi.Models;

public class User : IdentityUser
{
    [Key]
        public int UserId { get; set; }
        public required string UserType { get; set; }
        [Required(ErrorMessage = "Name is required."), MaxLength(20)]
        public required string Name { get; set; }
	
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public required string Password { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;


        public ICollection<AssetAllocation>? Allocations { get; set; }
        public ICollection<ServiceRequest>? ServiceRequests { get; set; }
        
        // public ICollection<AssetsRequests>? AssetsRequests { get; set; }
        public ICollection<AuditRequest>? AuditRequests { get; set; }
    
}