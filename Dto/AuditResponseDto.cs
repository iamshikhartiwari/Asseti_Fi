namespace Asseti_Fi.Dto;

public class AuditResponseDto
{
    public string AuditStatus { get; set; } // "Pending", "Approved", "Rejected"
    public string Comments { get; set; } 
}