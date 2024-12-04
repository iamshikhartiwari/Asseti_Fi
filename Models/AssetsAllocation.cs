using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asseti_Fi.Models{
    public class AssetAllocation
    {
        [Key]
        public int AllocationId { get; set; }
		
        public int AssetId { get; set; }
        public int UserId { get; set; }
        public DateTime AllocationDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
        public required string AllocationStatus { get; set; } 


        public Asset? Asset { get; set; }
        public User? User { get; set; }
    }
}