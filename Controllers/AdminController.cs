using Asseti_Fi.Dto;
using Asseti_Fi.Repositories.AdminRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]  // Only Admin users can access this controller
    public class AdminController : ControllerBase
    {
        private readonly IAssetManagementRepo _assetManagementRepo;
        private readonly IAuditManagementRepo _auditManagementRepo;
        private readonly IUserManagementRepo _userManagementRepo;

        // Constructor to inject repositories
        public AdminController(IAssetManagementRepo assetManagementRepo,
                               IAuditManagementRepo auditManagementRepo,
                               IUserManagementRepo userManagementRepo)
        {
            _assetManagementRepo = assetManagementRepo;
            _auditManagementRepo = auditManagementRepo;
            _userManagementRepo = userManagementRepo;
        }

        // Asset Management Endpoints

        [HttpGet("assets")]
        public async Task<IActionResult> GetAllAssets()
        {
            return await _assetManagementRepo.GetAllAssets();
        }

        [HttpPost("assets")]
        public async Task<IActionResult> AddAsset([FromBody] AddAssetDto addAssetDto)
        {
            return await _assetManagementRepo.AddAsset(addAssetDto);
        }

        [HttpPut("assets/{id}")]
        public async Task<IActionResult> UpdateAsset(int id, [FromBody] UpdateAssetDto updateAssetDto)
        {
            return await _assetManagementRepo.UpdateAssetById(id, updateAssetDto);
        }

        [HttpDelete("assets/{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            return await _assetManagementRepo.DeleteAssetById(id);
        }

        // Audit Management Endpoints

        [HttpGet("audit-requests")]
        public async Task<IActionResult> GetAllAuditRequests()
        {
            return await _auditManagementRepo.GetAllAuditRequests();
        }

        [HttpPost("audit-requests")]
        public async Task<IActionResult> AddAuditRequest([FromBody] AuditRequestDto auditRequestDto)
        {
            return await _auditManagementRepo.AddAuditRequest(auditRequestDto);
        }

        [HttpPut("audit-requests/{id}")]
        public async Task<IActionResult> UpdateAuditRequest(int id, [FromBody] AuditRequestDto auditRequestDto)
        {
            return await _auditManagementRepo.UpdateAuditRequest(id, auditRequestDto);
        }

        [HttpDelete("audit-requests/{id}")]
        public async Task<IActionResult> DeleteAuditRequest(int id)
        {
            return await _auditManagementRepo.DeleteAuditRequest(id);
        }

        // User Management Endpoints

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _userManagementRepo.GetAllUsers();
        }

        [HttpPost("users")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto addUserDto)
        {
            return await _userManagementRepo.AddUser(addUserDto);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] AddUserDto updateUserDto)
        {
            return await _userManagementRepo.UpdateUserById(id, updateUserDto);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return await _userManagementRepo.DeleteUserById(id);
        }
    }
}
