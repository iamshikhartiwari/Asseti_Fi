using Asseti_Fi.Dto;
using Asseti_Fi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Asseti_Fi.Aessiti_Fi_DBContext;
using Microsoft.EntityFrameworkCore;

namespace Asseti_Fi.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AesstsDBContext _context;  // Use your custom DbContext
        private readonly IConfiguration _configuration;

        public AuthRepository(AesstsDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> RegisterAsync(RegisterUserDto registerDto)
        {
            // Create the custom user (password is stored in plain text)
            var user = new User
            {
                Name = registerDto.Username,
                Email = registerDto.Email,
                UserType = registerDto.Role, // Assuming UserType maps to role (Admin/User)
                ContactNumber = registerDto.ContactNumber,
                Address = registerDto.Address,
                DateCreated = DateTime.UtcNow,
                PasswordHash = registerDto.Password  // Store password as plain text (NOT recommended for production!)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Optionally, assign a role manually or use a role table if necessary
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == registerDto.Role);
            if (role != null)
            {
                user.UserType = role.RoleName;  // Store role as part of the User model if needed
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == loginDto.Username);
            if (user == null || user.PasswordHash != loginDto.Password)  // Compare plain text passwords directly
                return null;

            // Create claims based on the user data
            var userRoles = new List<string> { user.UserType }; // Manually assign roles

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserType", user.UserType),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add roles as claims
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Generate JWT Token
            var token = GenerateToken(authClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            return new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
