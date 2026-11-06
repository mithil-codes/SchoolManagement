using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetByIdAsync(int id);
        Task<(List<Teacher> Teachers, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Teacher> AddAsync(Teacher teacher);
        Task UpdateAsync(Teacher teacher);
        Task DeleteAsync(Teacher teacher);
        Task<Teacher?> GetByApplicationUserIdAsync(string applicationUserId);
    }
}
