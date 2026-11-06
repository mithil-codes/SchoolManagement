using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface ISubjectService
    {
        Task<SubjectDto?> GetByIdAsync(int id);
        Task<PagedResultDto<SubjectDto>> GetAllAsync(PaginationParams pagination);
        Task<SubjectDto> CreateAsync(CreateSubjectDto dto);
        Task<bool> UpdateAsync(int id, UpdateSubjectDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
