using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Fee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IFeeService
    {
        Task<FeeDto?> GetByIdAsync(int id);
        Task<PagedResultDto<FeeDto>> GetAllAsync(PaginationParams pagination);
        Task<List<FeeDto>> GetByStudentIdAsync(int studentId);
        Task<FeeDto> CreateAsync(CreateFeeDto dto);
        Task<(bool Success, string Message, FeeDto? Data)> PayFeeAsync(int id, PayFeeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
