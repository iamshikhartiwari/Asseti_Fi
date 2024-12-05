using Asseti_Fi.Dto;
using Asseti_Fi.Repositories.AuthRepository;
using Microsoft.AspNetCore.Mvc;

namespace Asseti_Fi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;


        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        // public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        // {
        //     var result = await _authRepository.RegisterAsync(registerDto);
        //
        //     if (!result.Success)
        //     {
        //         return BadRequest(result.Errors);
        //     }
        //
        //     return Ok("User registered successfully");
        // }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _authRepository.LoginAsync(loginDto);

                if (token == null)
                {
                    return Unauthorized("Invalid credentials");
                }

                return Ok(new { token });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}