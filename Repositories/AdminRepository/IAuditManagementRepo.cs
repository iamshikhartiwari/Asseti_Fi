using Asseti_Fi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Repositories.AdminRepository;

public interface IAuditManagementRepo
{
    Task<IActionResult> GetAllAuditRequests();
    Task<IActionResult> AddAuditRequest(AuditRequestDto auditRequestDto);
    Task<IActionResult> UpdateAuditRequest(int id, AuditRequestDto auditRequestDto);
    Task<IActionResult> DeleteAuditRequest(int id);
}