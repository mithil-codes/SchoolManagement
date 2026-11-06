using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Application.Interfaces;
using System.Security.Claims;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
      
            private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Teacher")]
            public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
            {
                var result = await _studentService.GetAllAsync(pagination);
                return Ok(result);
            }

            [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Teacher,Student")]
            public async Task<IActionResult> GetById(int id)
            {

            // ---- TEMPORARY TEST CODE — baad me hata dena ----
            if (id == 999)
            {
                throw new Exception("This is a test exception to check Global Exception Handling");
            }
            // ---- TEST CODE END ----
            var student = await _studentService.GetByIdAsync(id);
                if (student == null) return NotFound(new { Message = "Student not found" });
                return Ok(student);
            }

            [HttpPost]
        [Authorize(Roles ="Admin")]
            public async Task<IActionResult> Create(CreateStudentDto dto)
            {
                var created = await _studentService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }

            [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
            public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
            {
                var success = await _studentService.UpdateAsync(id, dto);
                if (!success) return NotFound(new { Message = "Student not found" });
                return NoContent();
            }

            [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]    
            public async Task<IActionResult> Delete(int id)
            {
                var success = await _studentService.DeleteAsync(id);
                if (!success) return NotFound(new { Message = "Student not found" });
                return NoContent();
            }

        [HttpPost("{id}/create-login")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateLogin(int id, CreateLoginDto dto)
        {
            var (success, message) = await _studentService.CreateLoginAsync(id, dto);
            if (!success) return BadRequest(new { Message = message });
            return Ok(new { Message = message });
        }

        [HttpGet("my-profile")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var student = await _studentService.GetByApplicationUserIdAsync(userId);
            if (student == null) return NotFound(new { Message = "No student profile linked to this account" });

            return Ok(student);
        }
    }
}
