using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Dto;
using Asseti_Fi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asseti_Fi.Repositories.CommonRepository;

public class AssetAllocationRepo : IAssetAllocationRepo
{
    private IAssetAllocationRepo _assetAllocationRepoImplementation;
    private readonly AesstsDBContext dbContext;

    public AssetAllocationRepo(AesstsDBContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<IActionResult> GetAllAssetAllocations()
        {
            var allocations = await dbContext.AssetAllocations.ToListAsync();
            return new OkObjectResult(allocations);
        }

        public async Task<IActionResult> AddAssetAllocation(AddAssetAllocationDto assetAllocationDto)
        {
            var newAllocation = new AssetAllocation
            {
                AssetId = assetAllocationDto.AssetId,
                UserId = assetAllocationDto.UserId,
                AllocationDate = assetAllocationDto.AllocationDate,
                ReturnDate = assetAllocationDto.ReturnDate,
                AllocationStatus = assetAllocationDto.AllocationStatus
            };

            await dbContext.AssetAllocations.AddAsync(newAllocation);
            await dbContext.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetAllAssetAllocations), "AssetAllocation", new { id = newAllocation.AllocationId }, newAllocation);
        }

        public async Task<IActionResult> UpdateAssetAllocation(int id, AddAssetAllocationDto assetAllocationDto)
        {
            var allocation = await dbContext.AssetAllocations.FindAsync(id);
            if (allocation == null)
                return new NotFoundResult();

            allocation.AssetId = assetAllocationDto.AssetId;
            allocation.UserId = assetAllocationDto.UserId;
            allocation.AllocationDate = assetAllocationDto.AllocationDate;
            allocation.ReturnDate = assetAllocationDto.ReturnDate;
            allocation.AllocationStatus = assetAllocationDto.AllocationStatus;

            dbContext.AssetAllocations.Update(allocation);
            await dbContext.SaveChangesAsync();

            return new OkObjectResult(allocation);
        }

        public async Task<IActionResult> DeleteAssetAllocation(int id)
        {
            var allocation = await dbContext.AssetAllocations.FindAsync(id);
            if (allocation == null)
                return new NotFoundResult();

            dbContext.AssetAllocations.Remove(allocation);
            await dbContext.SaveChangesAsync();

            return new NoContentResult();
        }
    


    

}