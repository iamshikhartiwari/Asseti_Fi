using System.ComponentModel.DataAnnotations;

namespace Asseti_Fi.Dto
{
	public class UpdateAssetDto
	{
		[Required(ErrorMessage = "Name is required."), MaxLength(20)]
		public required string AssetName { get; set; }
		public string? AssetCategory { get; set; }
		public string? AssetModel { get; set; }
		public DateTime? ManufacturingDate { get; set; }
		public DateTime? ExpiryDate { get; set; }
		public decimal AssetValue { get; set; }
		public required string CurrentStatus { get; set; }
	}
}
