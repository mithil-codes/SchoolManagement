using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IExamService
    {
        Task<ExamDto?> GetByIdAsync(int id);
        Task<PagedResultDto<ExamDto>> GetAllAsync(PaginationParams pagination);
        Task<ExamDto> CreateAsync(CreateExamDto dto);
        Task<bool> UpdateAsync(int id, UpdateExamDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
