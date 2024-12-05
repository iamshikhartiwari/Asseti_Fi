using Asseti_Fi.Aessiti_Fi_DBContext;
using Asseti_Fi.Dto;
using Asseti_Fi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asseti_Fi.Repositories.AdminRepository;

public class UserManagementRepo : IUserManagementRepo
{
    private IUserManagementRepo _userManagementRepoImplementation;
    private readonly AesstsDBContext _dbContext;
    private readonly UserManager<User> _userManager;

    
    public UserManagementRepo(AesstsDBContext dbContext, UserManager<User> userManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }


    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return new OkObjectResult(users);
    }

    public async Task<IActionResult> AddUser(AddUserDto addUserDto)
    {
        var newUser = new User
        {
            Name = addUserDto.Name,
            Email = addUserDto.Email,
            UserType = addUserDto.UserType,
            ContactNumber = addUserDto.ContactNumber,
            Address = addUserDto.Address,
            DateCreated = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(newUser, addUserDto.Password); // Identity handles password hashing

        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        // Optionally, assign roles if needed
        if (!string.IsNullOrEmpty(addUserDto.UserType))
        {
            await _userManager.AddToRoleAsync(newUser, addUserDto.UserType);
        }

        return new CreatedAtActionResult(nameof(GetAllUsers), "UserManagement", new { id = newUser.UserId }, newUser);
    }

    public async Task<IActionResult> UpdateUserById(string id, AddUserDto updateUserDto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return new NotFoundResult();

        user.Name = updateUserDto.Name;
        user.Email = updateUserDto.Email;
        user.UserType = updateUserDto.UserType;
        user.ContactNumber = updateUserDto.ContactNumber;
        user.Address = updateUserDto.Address;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        return new OkObjectResult(user);
    }

    public async Task<IActionResult> DeleteUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return new NotFoundResult();

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        return new NoContentResult();
    }
}