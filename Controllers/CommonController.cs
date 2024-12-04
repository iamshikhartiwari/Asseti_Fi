using Asseti_Fi.Dto;
using Asseti_Fi.Repositories.CommonRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Allows access to both Admin and User roles
    public class CommonController : ControllerBase
    {
        private readonly IAssetAllocationRepo _assetAllocationRepo;
        private readonly IServiceRequestsRepo _serviceRequestRepo;

        // Constructor to inject repositories
        public CommonController(IAssetAllocationRepo assetAllocationRepo, IServiceRequestsRepo serviceRequestRepo)
        {
            _assetAllocationRepo = assetAllocationRepo;
            _serviceRequestRepo = serviceRequestRepo;
        }

        // Asset Allocation Endpoints

        [HttpGet("asset-allocations")]
        public async Task<IActionResult> GetAllAssetAllocations()
        {
            return await _assetAllocationRepo.GetAllAssetAllocations();
        }

        [HttpPost("asset-allocations")]
        public async Task<IActionResult> AddAssetAllocation([FromBody] AddAssetAllocationDto assetAllocationDto)
        {
            return await _assetAllocationRepo.AddAssetAllocation(assetAllocationDto);
        }

        [HttpPut("asset-allocations/{id}")]
        public async Task<IActionResult> UpdateAssetAllocation(int id, [FromBody] AddAssetAllocationDto assetAllocationDto)
        {
            return await _assetAllocationRepo.UpdateAssetAllocation(id, assetAllocationDto);
        }

        [HttpDelete("asset-allocations/{id}")]
        public async Task<IActionResult> DeleteAssetAllocation(int id)
        {
            return await _assetAllocationRepo.DeleteAssetAllocation(id);
        }

        // Service Request Endpoints

        [HttpGet("service-requests")]
        public async Task<IActionResult> GetAllServiceRequests()
        {
            return await _serviceRequestRepo.GetAllServiceRequests();
        }



        [HttpPut("service-requests/{id}")]
        public async Task<IActionResult> UpdateServiceRequest(int id, [FromBody] ServiceRequestDto serviceRequestDto)
        {
            return await _serviceRequestRepo.UpdateServiceRequest(id, serviceRequestDto);
        }

        [HttpDelete("service-requests/{id}")]
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            return await _serviceRequestRepo.DeleteServiceRequest(id);
        }
    }


}
