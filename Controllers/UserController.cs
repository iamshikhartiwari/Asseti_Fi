using Asseti_Fi.Dto;
using Asseti_Fi.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]  // Only Users can access this controller
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        // Constructor to inject the UserRepo dependency
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        // Respond to an audit request
        [HttpPost("audit-requests/{auditRequestId}/respond")]
        public async Task<IActionResult> RespondToAuditRequest(int auditRequestId, [FromBody] AuditResponseDto auditResponseDto)
        {
            return await _userRepo.RespondToAuditRequest(auditRequestId, auditResponseDto);
        }
    }
}
