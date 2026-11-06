using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Subject;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectsController : ControllerBase
    {

        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Teacher,Student")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _subjectService.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var subject = await _subjectService.GetByIdAsync(id);
            if (subject == null) return NotFound(new { Message = "Subject not found" });
            return Ok(subject);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateSubjectDto dto)
        {
            var created = await _subjectService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateSubjectDto dto)
        {
            var success = await _subjectService.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "Subject not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _subjectService.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Subject not found" });
            return NoContent();
        }
    }
}

