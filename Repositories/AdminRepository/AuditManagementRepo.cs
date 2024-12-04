using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Dto;
using Asseti_Fi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asseti_Fi.Repositories.AdminRepository;

public class AuditManagementRepo : IAuditManagementRepo
{
    private IAuditManagementRepo _auditManagementRepoImplementation;
    private readonly AesstsDBContext dbContext;

    public AuditManagementRepo(AesstsDBContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<IActionResult> GetAllAuditRequests()
    {
        var auditRequests = await dbContext.AuditRequests.ToListAsync();
        return new OkObjectResult(auditRequests);
    }

    public async Task<IActionResult> AddAuditRequest(AuditRequestDto auditRequestDto)
    {
        
        var newRequest = new AuditRequest 
        {
            AdminId = auditRequestDto.AdminId,
            UserId = auditRequestDto.UserId,
            AuditStatus = auditRequestDto.AuditStatus,
            AuditDate = auditRequestDto.AuditDate
        };

        await dbContext.AuditRequests.AddAsync(newRequest); 
        await dbContext.SaveChangesAsync();

        return new OkObjectResult(newRequest);
    }


    public async Task<IActionResult> UpdateAuditRequest(int id, AuditRequestDto auditRequestDto)
    {
        var request = await dbContext.AuditRequests.FindAsync(id);
        if (request == null) return new NotFoundResult();

        request.AuditStatus = auditRequestDto.AuditStatus;
        dbContext.AuditRequests.Update(request);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(request);
    }

    public async Task<IActionResult> DeleteAuditRequest(int id)
    {
        var request = await dbContext.AuditRequests.FindAsync(id);
        if (request == null) return new NotFoundResult();

        dbContext.AuditRequests.Remove(request);
        await dbContext.SaveChangesAsync();
        return new NoContentResult();
    }
}