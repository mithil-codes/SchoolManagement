using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Fee;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        private readonly IFeeService _service;

        public FeesController(IFeeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _service.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound(new { Message = "Fee record not found" });
            return Ok(dto);
        }

        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var result = await _service.GetByStudentIdAsync(studentId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateFeeDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPost("{id}/pay")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PayFee(int id, PayFeeDto dto)
        {
            var (success, message, data) = await _service.PayFeeAsync(id, dto);
            if (!success) return BadRequest(new { Message = message });
            return Ok(new { Message = message, Data = data });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Fee record not found" });
            return NoContent();
        }
    }
}
