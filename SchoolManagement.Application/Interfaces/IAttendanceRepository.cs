using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<Attendance?> GetByIdAsync(int id);
        Task<(List<Attendance> Records, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<List<Attendance>> GetByStudentIdAsync(int studentId);
        Task<List<Attendance>> GetByClassAndDateAsync(int classId, DateTime date);
        Task<Attendance> AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(Attendance attendance);
        Task<bool> ExistsForStudentOnDateAsync(int studentId, DateTime date);
    }
}
