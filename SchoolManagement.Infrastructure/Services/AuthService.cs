using Microsoft.AspNetCore.Identity;
using SchoolManagement.Application.DTOs.Auth;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto { IsSuccess = false, Message = "Email already registered" };
            }

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Address = dto.Address,
                PinCode = dto.PinCode
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthResponseDto { IsSuccess = false, Message = errors };
            }

            // Flexible — Admin jo bhi role naam de, wahi ban jayega (exist nahi karta to create ho jayega)
            var roleExists = await _roleManager.RoleExistsAsync(dto.Role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));
            }

            await _userManager.AddToRoleAsync(user, dto.Role);

            return new AuthResponseDto { IsSuccess = true, Message = $"Registration successful as {dto.Role}" };
        }


        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new LoginResponseDto { IsSuccess = false, Message = "Invalid email or password" };
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
            {
                return new LoginResponseDto { IsSuccess = false, Message = "Invalid email or password" };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);

            return new LoginResponseDto
            {
                IsSuccess = true,
                Message = "Login successful",
                UserId = user.Id,
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Gender = user.Gender,
                Address = user.Address,
                Roles = roles,
                Token = token,
                TokenExpiry = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
    }

