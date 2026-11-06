using SchoolManagement.Application.DTOs.Class;
using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IClassService
    {
        Task<ClassDto?> GetByIdAsync(int id);
        Task<PagedResultDto<ClassDto>> GetAllAsync(PaginationParams pagination);
        Task<ClassDto> CreateAsync(CreateClassDto dto);
        Task<bool> UpdateAsync(int id, UpdateClassDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
