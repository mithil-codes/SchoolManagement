using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Notice;
using SchoolManagement.Application.Interfaces;
using System.Security.Claims;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeService _service;

        public NoticesController(INoticeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Teacher,Student")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _service.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpGet("active")]
        [Authorize(Roles ="Admin,Teacher,Student")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _service.GetActiveNoticesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Teacher,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound(new { Message = "Notice not found" });
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> Create(CreateNoticeDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var created = await _service.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> Update(int id, UpdateNoticeDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "Notice not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Notice not found" });
            return NoContent();
        }
    }
}
