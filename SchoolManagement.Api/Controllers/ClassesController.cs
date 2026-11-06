using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs.Class;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClassesController : ControllerBase
    {

        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService= classService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin,Teacher,Student")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _classService.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Teacher,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var classDto = await _classService.GetByIdAsync(id);
            if (classDto == null) return NotFound(new { Message = "Class not found" });
            return Ok(classDto);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create(CreateClassDto dto)
        {
            var created = await _classService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(int id, UpdateClassDto dto)
        {
            var success = await _classService.UpdateAsync(id, dto);
            if (!success) return NotFound(new { Message = "Class not found" });
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _classService.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Class not found" });
            return NoContent();
        }

        [HttpGet("public-list")]
        // KOI [Authorize] NAHI — public hai
        public async Task<IActionResult> GetPublicClassList()
        {
            var result = await _classService.GetAllAsync(new PaginationParams { PageNumber = 1, PageSize = 100 });
            var simplified = result.Items.Select(c => new { c.Id, c.ClassName, c.Section }).ToList();
            return Ok(simplified);
        }
    }
}

