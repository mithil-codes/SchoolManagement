using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface INoticeService
    {
        Task<NoticeDto?> GetByIdAsync(int id);
        Task<PagedResultDto<NoticeDto>> GetAllAsync(PaginationParams pagination);
        Task<List<NoticeDto>> GetActiveNoticesAsync();
        Task<NoticeDto> CreateAsync(CreateNoticeDto dto, string postedByUserId);
        Task<bool> UpdateAsync(int id, UpdateNoticeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
