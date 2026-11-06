using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {


        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<Attendance> BaseQuery() =>
            _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Class)
                .Include(a => a.MarkedByTeacher);

        public async Task<Attendance?> GetByIdAsync(int id) =>
            await BaseQuery().FirstOrDefaultAsync(a => a.Id == id);

        public async Task<(List<Attendance> Records, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = BaseQuery();
            var totalCount = await query.CountAsync();

            var records = await query
                .OrderByDescending(a => a.AttendanceDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (records, totalCount);
        }

        public async Task<List<Attendance>> GetByStudentIdAsync(int studentId) =>
            await BaseQuery().Where(a => a.StudentId == studentId).OrderByDescending(a => a.AttendanceDate).ToListAsync();

        public async Task<List<Attendance>> GetByClassAndDateAsync(int classId, DateTime date) =>
            await BaseQuery().Where(a => a.ClassId == classId && a.AttendanceDate.Date == date.Date).ToListAsync();

        public async Task<Attendance> AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsForStudentOnDateAsync(int studentId, DateTime date) =>
            await _context.Attendances.AnyAsync(a => a.StudentId == studentId && a.AttendanceDate.Date == date.Date);
    }
}

