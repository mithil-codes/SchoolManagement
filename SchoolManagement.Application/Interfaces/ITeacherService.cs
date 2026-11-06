using SchoolManagement.Application.DTOs.ClassSubject;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Application.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<TeacherDto?> GetByIdAsync(int id);
        Task<PagedResultDto<TeacherDto>> GetAllAsync(PaginationParams pagination);
        Task<TeacherDto> CreateAsync(CreateTeacherDto dto);
        Task<bool> UpdateAsync(int id, UpdateTeacherDto dto);
        Task<bool> DeleteAsync(int id);
        Task<(bool Success, string Message)> CreateLoginAsync(int teacherId, CreateLoginDto dto);
        Task<TeacherDto?> GetByApplicationUserIdAsync(string applicationUserId);

        Task<List<ClassSubjectDto>> GetMyClassSubjectsAsync(string applicationUserId);
    }
}
