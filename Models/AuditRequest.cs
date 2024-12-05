using System.ComponentModel.DataAnnotations;

namespace Asseti_Fi.Models;

public class AuditRequest
{
    [Key]
    public int AuditRequestId { get; set; }  // Primary Key
    public int UserId { get; set; }  // The user associated with this audit request
    
    public int AdminId { get; set; }
    public string AuditStatus { get; set; }  // Status of the audit (e.g., Pending, Approved, Rejected)
    public DateTime AuditDate { get; set; }  // Date the audit was requested
    public string Comments { get; set; }  // Optional comments regarding the audit request
    
    public User User { get; set; }
    public User Admin { get; set; }


}