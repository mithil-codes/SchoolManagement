using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SchoolManagement.Application.DTOs.Admission;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdmissionApplicationsController : ControllerBase
    {
        private readonly IAdmissionService _service;

        public AdmissionApplicationsController(IAdmissionService service)
        {
            _service = service;
        }

        [HttpPost("apply")]
        // KOI [Authorize] NAHI — public endpoint hai
        public async Task<IActionResult> Apply(SubmitApplicationDto dto)
        {
            var result = await _service.SubmitApplicationAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _service.GetAllAsync(pagination);
            return Ok(result);
        }

        [HttpPost("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var (success, message) = await _service.ApproveAsync(id);
            if (!success) return BadRequest(new { Message = message });
            return Ok(new { Message = message });
        }

        [HttpPost("{id}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            var (success, message) = await _service.RejectAsync(id);
            if (!success) return BadRequest(new { Message = message });
            return Ok(new { Message = message });
        }
    }
}
