using Asseti_Fi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Repositories.CommonRepository;

public interface IServiceRequestsRepo
{
    Task<IActionResult> GetAllServiceRequests();
    Task<IActionResult> AddServiceRequest(ServiceRequestDto serviceRequestDto);
    Task<IActionResult> UpdateServiceRequest(int id, ServiceRequestDto serviceRequestDto);
    Task<IActionResult> DeleteServiceRequest(int id);
}