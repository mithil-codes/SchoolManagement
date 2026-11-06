using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IMarksRepository
    {
        Task<Marks?> GetByIdAsync(int id);
        Task<(List<Marks> Records, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<List<Marks>> GetByStudentAndExamAsync(int studentId, int examId);
        Task<Marks> AddAsync(Marks marks);
        Task UpdateAsync(Marks marks);
        Task DeleteAsync(Marks marks);
        Task<bool> ExistsAsync(int examId, int studentId, int subjectId);
    }
}
