using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Student;

using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto?> GetByIdAsync(int id);
        Task<PagedResultDto<StudentDto>> GetAllAsync(PaginationParams pagination);
        Task<StudentDto> CreateAsync(CreateStudentDto dto);
        Task<bool> UpdateAsync(int id, UpdateStudentDto dto);
        Task<bool> DeleteAsync(int id);
        Task<(bool Success, string Message)> CreateLoginAsync(int studentId, CreateLoginDto dto);
        Task<StudentDto?> GetByApplicationUserIdAsync(string applicationUserId);
    }
}
