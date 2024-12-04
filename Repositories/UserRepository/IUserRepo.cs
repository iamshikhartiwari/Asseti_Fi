using Asseti_Fi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Repositories.UserRepository;

public interface IUserRepo
{
    Task<IActionResult> RespondToAuditRequest(int auditRequestId, AuditResponseDto auditResponseDto);

}