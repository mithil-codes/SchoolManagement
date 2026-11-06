using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]   // ab sirf Admin hi naya Admin bana sakta hai
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result.IsSuccess) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (!result.IsSuccess) return Unauthorized(result);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("profile")]
     // ye attribute bolta hai — bina valid JWT token ke access mat do
    public IActionResult GetProfile()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                     ?? User.FindFirst("sub")?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var roles = User.FindAll(System.Security.Claims.ClaimTypes.Role).Select(r => r.Value);

        return Ok(new
        {
            UserId = userId,
            Email = email,
            Roles = roles
        });
    }
}
}
