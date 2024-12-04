using System.ComponentModel.DataAnnotations;
using Asseti_Fi.Models;

namespace Asseti_Fi.Dto;

public class AuditRequestDto
{
    public int AuditId { get; set; } 

    [Required(ErrorMessage = "AdminId is required.")]
    public int AdminId { get; set; }

    [Required(ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }
    public DateTime AuditDate { get; set; } = DateTime.UtcNow;
    public User Admin { get; set; }
    public User User { get; set; }


    [Required(ErrorMessage = "AuditStatus is required.")]
    public string AuditStatus { get; set; }

    
}