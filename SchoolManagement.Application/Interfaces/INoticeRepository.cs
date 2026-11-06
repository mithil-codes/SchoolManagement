using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface INoticeRepository
    {
        Task<Notice?> GetByIdAsync(int id);
        Task<(List<Notice> Notices, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<List<Notice>> GetActiveNoticesAsync();
        Task<Notice> AddAsync(Notice notice);
        Task UpdateAsync(Notice notice);
        Task DeleteAsync(Notice notice);
    }
}
