using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Exam;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class ExamsController:ControllerBase
    {
        private readonly IExamService _examService;

        public ExamsController(IExamService examService)
        {
            _examService = examService; 
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _examService.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var exam = await _examService.GetByIdAsync(id);
            if (exam == null) return NotFound(new { Message = "Exam not found" });
            return Ok(exam);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateExamDto dto)
        {
            var created = await _examService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateExamDto dto)
        {
            var success = await _examService.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "Exam not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _examService.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Exam not found" });
            return NoContent();
        }
    }
}

