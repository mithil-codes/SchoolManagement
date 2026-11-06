using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IFeeRepository
    {
        Task<Fee?> GetByIdAsync(int id);
        Task<(List<Fee> Fees, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<List<Fee>> GetByStudentIdAsync(int studentId);
        Task<Fee> AddAsync(Fee fee);
        Task UpdateAsync(Fee fee);
        Task DeleteAsync(Fee fee);
    }
}
