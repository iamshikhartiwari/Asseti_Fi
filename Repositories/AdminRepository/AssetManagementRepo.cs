using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Dto;
using Asseti_Fi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asseti_Fi.Repositories.AdminRepository;

public class AssetManagementRepo : IAssetManagementRepo
{
    private IAssetManagementRepo _assetManagementRepoImplementation;
    
    private readonly AesstsDBContext dbContext;

    public AssetManagementRepo(AesstsDBContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task<IActionResult> GetAllAssets()
    {
        var assets = await dbContext.Assets.ToListAsync();
        return new OkObjectResult(assets);
    }


    public async Task<IActionResult> AddAsset(AddAssetDto addAssetDto)
    {
        var newAsset = new Asset
        {
            AssetName = addAssetDto.AssetName,
            AssetCategory = addAssetDto.AssetCategory,
            AssetModel = addAssetDto.AssetModel,
            ManufacturingDate = addAssetDto.ManufacturingDate,
            ExpiryDate = addAssetDto.ExpiryDate,
            AssetValue = addAssetDto.AssetValue,
            CurrentStatus = addAssetDto.CurrentStatus
        };

        await dbContext.Assets.AddAsync(newAsset);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(newAsset);
    }

    public async Task<IActionResult> UpdateAssetById(int id, UpdateAssetDto updateAssetDto)
    {
        var asset = await dbContext.Assets.FindAsync(id);
        if (asset == null) return new NotFoundResult();

        asset.AssetName = updateAssetDto.AssetName;
        asset.AssetCategory = updateAssetDto.AssetCategory;
        asset.AssetModel = updateAssetDto.AssetModel;
        asset.ManufacturingDate = updateAssetDto.ManufacturingDate;
        asset.ExpiryDate = updateAssetDto.ExpiryDate;
        asset.AssetValue = updateAssetDto.AssetValue;
        asset.CurrentStatus = updateAssetDto.CurrentStatus;

        dbContext.Assets.Update(asset);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(asset);
    }


    public async Task<IActionResult> DeleteAssetById(int id)
    {
        Console.WriteLine($"Attempting to delete asset with ID: {id}");

        var asset = await dbContext.Assets.FindAsync(id);
        if (asset == null)
        {
            Console.WriteLine($"Asset with ID {id} not found.");
            return new NotFoundResult();
        }

        dbContext.Assets.Remove(asset);
        await dbContext.SaveChangesAsync();
        return new NoContentResult();
    }

}