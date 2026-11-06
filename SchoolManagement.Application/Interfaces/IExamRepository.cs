using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IExamRepository
    {
        Task<Exam?> GetByIdAsync(int id);
        Task<(List<Exam> Exams, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Exam> AddAsync(Exam exam);
        Task UpdateAsync(Exam exam);
        Task DeleteAsync(Exam exam);
    }
}
