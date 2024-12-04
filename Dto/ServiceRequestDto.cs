using System.ComponentModel.DataAnnotations;

namespace Asseti_Fi.Dto;

public class ServiceRequestDto
    {
        public int RequestId { get; set; } // For updates

        [Required(ErrorMessage = "AssetId is required.")]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        public string? Description { get; set; }

        public string? IssueType { get; set; } // Examples: Malfunction, Repair, Maintenance

        [Required(ErrorMessage = "RequestStatus is required.")]
        public string RequestStatus { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    }


