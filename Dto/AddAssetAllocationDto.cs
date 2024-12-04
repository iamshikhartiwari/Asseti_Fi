namespace Asseti_Fi.Dto
{
	public class AddAssetAllocationDto
	{
		public int AssetId { get; set; }
		public int UserId { get; set; }
		public DateTime AllocationDate { get; set; } = DateTime.UtcNow;
		public DateTime? ReturnDate { get; set; }
		public required string AllocationStatus { get; set; }
	}
}
