using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject?> GetByIdAsync(int id);
        Task<(List<Subject> Subjects, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Subject> AddAsync(Subject subject);
        Task UpdateAsync(Subject subject);
        Task DeleteAsync(Subject subject);
     
    }
}
