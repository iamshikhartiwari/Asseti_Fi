using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Dto;
using Asseti_Fi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asseti_Fi.Repositories.CommonRepository;

public class ServiceRequestRepo : IServiceRequestsRepo
{
    private IServiceRequestsRepo _serviceRequestsRepoImplementation;
    private readonly AesstsDBContext dbContext;
    private IAssetAllocationRepo _assetAllocationRepoImplementation1;

    public ServiceRequestRepo(AesstsDBContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<IActionResult> GetAllServiceRequests()
        {
            var serviceRequests = await dbContext.ServiceRequests.ToListAsync();
            return new OkObjectResult(serviceRequests);
        }

        public async Task<IActionResult> AddServiceRequest(ServiceRequestDto serviceRequestDto)
        {
            var newRequest = new ServiceRequest
            {
                AssetId = serviceRequestDto.AssetId,
                UserId = serviceRequestDto.UserId,
                Description = serviceRequestDto.Description,
                IssueType = serviceRequestDto.IssueType,
                RequestStatus = serviceRequestDto.RequestStatus,
                RequestDate = serviceRequestDto.RequestDate
            };

            await dbContext.ServiceRequests.AddAsync(newRequest);
            await dbContext.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetAllServiceRequests), "ServiceRequest", new { id = newRequest.RequestId }, newRequest);
        }

        public async Task<IActionResult> UpdateServiceRequest(int id, ServiceRequestDto serviceRequestDto)
        {
            var request = await dbContext.ServiceRequests.FindAsync(id);
            if (request == null)
                return new NotFoundResult();

            request.Description = serviceRequestDto.Description;
            request.IssueType = serviceRequestDto.IssueType;
            request.RequestStatus = serviceRequestDto.RequestStatus;
            request.RequestDate = serviceRequestDto.RequestDate;

            dbContext.ServiceRequests.Update(request);
            await dbContext.SaveChangesAsync();

            return new OkObjectResult(request);
        }

        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            var request = await dbContext.ServiceRequests.FindAsync(id);
            if (request == null)
                return new NotFoundResult();

            dbContext.ServiceRequests.Remove(request);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
    
}