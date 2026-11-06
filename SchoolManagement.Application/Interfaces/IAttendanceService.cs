using SchoolManagement.Application.DTOs.AttendaceDto;
using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceDto?> GetByIdAsync(int id);
        Task<PagedResultDto<AttendanceDto>> GetAllAsync(PaginationParams pagination);
        Task<List<AttendanceDto>> GetByClassAndDateAsync(int classId, DateTime date);
        Task<AttendancePercentageDto> GetAttendancePercentageAsync(int studentId);
        Task<(bool Success, string Message, AttendanceDto? Data)> CreateAsync(CreateAttendanceDto dto);
        Task<bool> UpdateAsync(int id, UpdateAttendanceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
