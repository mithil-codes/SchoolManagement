using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IClassRepository
    {
        Task<Class?> GetByIdAsync(int id);
        Task<(List<Class> Classes, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Class> AddAsync(Class classEntity);
        Task UpdateAsync(Class classEntity);
        Task DeleteAsync(Class classEntity);
    }
}
