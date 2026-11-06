using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.ClassSubject;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Infrastructure.Repositories;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace SchoolManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClassSubjectsController : ControllerBase
    {
        private readonly IClassSubjectService _service;

        public ClassSubjectsController(IClassSubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _service.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound(new { Message = "ClassSubject not found" });
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create(CreateClassSubjectDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateClassSubjectDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "ClassSubject not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "ClassSubject not found" });
            return NoContent();
        }
    }
}







