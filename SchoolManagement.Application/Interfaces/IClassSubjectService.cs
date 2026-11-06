using SchoolManagement.Application.DTOs.ClassSubject;
using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IClassSubjectService
    {
        Task<ClassSubjectDto?> GetByIdAsync(int id);
        Task<PagedResultDto<ClassSubjectDto>> GetAllAsync(PaginationParams pagination);
        Task<ClassSubjectDto> CreateAsync(CreateClassSubjectDto dto);
        Task<bool> UpdateAsync(int id, UpdateClassSubjectDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
