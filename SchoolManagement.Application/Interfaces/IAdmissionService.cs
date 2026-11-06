using SchoolManagement.Application.DTOs.Admission;
using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IAdmissionService
    {
        Task<ApplicationDto> SubmitApplicationAsync(SubmitApplicationDto dto);
        Task<PagedResultDto<ApplicationDto>> GetAllAsync(PaginationParams pagination);
        Task<(bool Success, string Message)> ApproveAsync(int id);
        Task<(bool Success, string Message)> RejectAsync(int id);
    }
}

