using System.ComponentModel.DataAnnotations;

namespace Asseti_Fi.Models;
public class AssetsRequests
{

        [Key]
        public int AuditId { get; set; }
        public int AdminId { get; set; }
        public int UserId { get; set; }
        public DateTime AuditDate { get; set; } = DateTime.UtcNow;

        public User? Admin { get; set; }
        public User? User { get; set; }
        
        public string? AuditStatus { get; set; }


}

