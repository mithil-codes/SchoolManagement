using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Application.DTOs.Teacher;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure.Services;
using System.Security.Claims;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeachersController : ControllerBase
    {


        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _teacherService.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null) return NotFound(new { Message = "Teacher not found" });
            return Ok(teacher);
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create(CreateTeacherDto dto)
        {
            var created = await _teacherService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Update(int id, UpdateTeacherDto dto)
        {
            var success = await _teacherService.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "Teacher not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _teacherService.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Teacher not found" });
            return NoContent();
        }


        [HttpPost("{id}/create-login")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLogin(int id, CreateLoginDto dto)
        {
            var (success, message) = await _teacherService.CreateLoginAsync(id, dto);
            if (!success) return BadRequest(new { Message = message });
            return Ok(new { Message = message });
        }


[HttpGet("my-profile")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var teacher = await _teacherService.GetByApplicationUserIdAsync(userId);
        if (teacher == null) return NotFound(new { Message = "No teacher profile linked to this account" });

        return Ok(teacher);
    }

        [HttpGet("my-classes")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetMyClassSubjects()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _teacherService.GetMyClassSubjectsAsync(userId);
            return Ok(result);
        }
    }
}   


