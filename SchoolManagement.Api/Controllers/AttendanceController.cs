using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.AttendaceDto;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendanceController : ControllerBase
    {

        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _service.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound(new { Message = "Attendance record not found" });
            return Ok(dto);
        }

        [HttpGet("by-class-date")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetByClassAndDate([FromQuery] int classId, [FromQuery] DateTime date)
        {
            var result = await _service.GetByClassAndDateAsync(classId, date);
            return Ok(result);
        }

        [HttpGet("percentage/{studentId}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetPercentage(int studentId)
        {
            var result = await _service.GetAttendancePercentageAsync(studentId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(CreateAttendanceDto dto)
        {
            var (success, message, data) = await _service.CreateAsync(dto);
            if (!success) return BadRequest(new { Message = message });
            return CreatedAtAction(nameof(GetById), new { id = data!.Id }, data);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update(int id, UpdateAttendanceDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "Attendance record not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Attendance record not found" });
            return NoContent();
        }
    }
}
