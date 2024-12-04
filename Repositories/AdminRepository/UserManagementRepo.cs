using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Dto;
using Asseti_Fi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asseti_Fi.Repositories.AdminRepository;

public class UserManagementRepo : IUserManagementRepo
{
    private IUserManagementRepo _userManagementRepoImplementation;
    private readonly AesstsDBContext dbContext;
    
    public UserManagementRepo(AesstsDBContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public async Task<IActionResult> GetAllUsers()
    {
        var users = await dbContext.Users.ToListAsync();
        return new OkObjectResult(users);
    }

    public async Task<IActionResult> AddUser(AddUserDto addUserDto)
    {
        var newUser = new User
        {
            UserType = addUserDto.UserType,
            Name = addUserDto.Name,
            Email = addUserDto.Email,
            Password = addUserDto.Password, // Ensure hashing for production!
            ContactNumber = addUserDto.ContactNumber,
            Address = addUserDto.Address,
            DateCreated = DateTime.UtcNow
        };

        await dbContext.Users.AddAsync(newUser);
        await dbContext.SaveChangesAsync();
        return new CreatedAtActionResult(nameof(GetAllUsers), "UserManagement", new { id = newUser.UserId }, newUser);
    }

    public async Task<IActionResult> UpdateUserById(int id, AddUserDto updateUserDto)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null) return new NotFoundResult();

        user.UserType = updateUserDto.UserType;
        user.Name = updateUserDto.Name;
        user.Email = updateUserDto.Email;
        user.Password = updateUserDto.Password; // Ensure hashing for production!
        user.ContactNumber = updateUserDto.ContactNumber;
        user.Address = updateUserDto.Address;

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
        return new OkObjectResult(user);
    }

    public async Task<IActionResult> DeleteUserById(int id)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null) return new NotFoundResult();

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return new NoContentResult();
    }
}