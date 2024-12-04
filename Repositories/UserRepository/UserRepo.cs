using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Dto;
using Asseti_Fi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Repositories.UserRepository;

public class UserRepo : IUserRepo
{
    private IUserRepo _userRepoImplementation;

    private readonly AesstsDBContext dbContext;

    public UserRepo(AesstsDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    
    public async Task<IActionResult> RespondToAuditRequest(int auditRequestId, AuditResponseDto auditResponseDto)
    {
        // Find the audit request in the database
        AuditRequest auditRequest = await dbContext.AuditRequests.FindAsync(auditRequestId);
        if (auditRequest == null)
            return new NotFoundResult();

        // Update the audit request with the response details
        auditRequest.AuditStatus = auditResponseDto.AuditStatus;
        auditRequest.Comments = auditResponseDto.Comments;
        // auditRequest.ResponseDate = DateTime.UtcNow;

        // Save changes to the database
        dbContext.AuditRequests.Update(auditRequest);
        await dbContext.SaveChangesAsync();

        return new OkObjectResult(auditRequest);
    }
}