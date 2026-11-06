using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Marks;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarksController : ControllerBase
    {

        private readonly IMarksService _service;

        public MarksController(IMarksService service)
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
        [Authorize(Roles ="Admin,Teacher,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound(new { Message = "Marks record not found" });
            return Ok(dto);
        }

        [HttpGet("report-card")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetReportCard([FromQuery] int studentId, [FromQuery] int examId)
        {
            var reportCard = await _service.GetReportCardAsync(studentId, examId);
            if (reportCard == null) return NotFound(new { Message = "No marks found for this student/exam" });
            return Ok(reportCard);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(CreateMarksDto dto)
        {
            var (success, message, data) = await _service.CreateAsync(dto);
            if (!success) return BadRequest(new { Message = message });
            return CreatedAtAction(nameof(GetById), new { id = data!.Id }, data);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Update(int id, UpdateMarksDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "Marks record not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Marks record not found" });
            return NoContent();
        }
    }
}
