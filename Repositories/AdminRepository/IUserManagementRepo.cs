using Asseti_Fi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Repositories.AdminRepository;

public interface IUserManagementRepo
{
    Task<IActionResult> GetAllUsers();
    Task<IActionResult> AddUser(AddUserDto addUserDto);
    Task<IActionResult> UpdateUserById(int id, AddUserDto updateUserDto);
    Task<IActionResult> DeleteUserById(int id);
}