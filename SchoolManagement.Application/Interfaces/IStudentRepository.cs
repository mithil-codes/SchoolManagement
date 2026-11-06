using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(int id);
        Task<(List<Student> Students, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Student> AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task<bool> ExistsAsync(int id);
        Task<Student?> GetByApplicationUserIdAsync(string applicationUserId);
        Task<int> GetHighestAdmissionNumberAsync();
    }
}
