using Asseti_Fi.Dto;
using Microsoft.AspNetCore.Identity;

namespace Asseti_Fi.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        Task<bool> RegisterAsync(RegisterUserDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}