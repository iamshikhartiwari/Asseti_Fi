using Microsoft.AspNetCore.Mvc;
using Asseti_Fi.Dto;

namespace Asseti_Fi.Repositories.CommonRepository;

public interface IAssetAllocationRepo
{
    Task<IActionResult> GetAllAssetAllocations();
    Task<IActionResult> AddAssetAllocation(AddAssetAllocationDto assetAllocationDto);
    Task<IActionResult> UpdateAssetAllocation(int id, AddAssetAllocationDto assetAllocationDto);
    Task<IActionResult> DeleteAssetAllocation(int id);
}