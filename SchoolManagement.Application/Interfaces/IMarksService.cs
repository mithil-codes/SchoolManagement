using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IMarksService
    {
        Task<MarksDto?> GetByIdAsync(int id);
        Task<PagedResultDto<MarksDto>> GetAllAsync(PaginationParams pagination);
        Task<StudentReportCardDto?> GetReportCardAsync(int studentId, int examId);
        Task<(bool Success, string Message, MarksDto? Data)> CreateAsync(CreateMarksDto dto);
        Task<bool> UpdateAsync(int id, UpdateMarksDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
