using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Repositories.AdminRepository;

public interface IAssetManagementRepo
{
    Task<IActionResult> GetAllAssets();
    Task<IActionResult> AddAsset(AddAssetDto addAssetDto);
    Task<IActionResult> UpdateAssetById(int id, UpdateAssetDto updateAssetDto);
    Task<IActionResult> DeleteAssetById(int id);
}