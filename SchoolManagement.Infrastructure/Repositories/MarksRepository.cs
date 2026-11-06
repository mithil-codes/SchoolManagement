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
    public class MarksRepository:IMarksRepository
    {
        private readonly ApplicationDbContext _context;
        public MarksRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<Marks> BaseQuery() =>
            _context.Marks
                .Include(m => m.Exam)
                .Include(m => m.Student)
                .Include(m => m.Subject);

        public async Task<Marks?> GetByIdAsync(int id) =>
            await BaseQuery().FirstOrDefaultAsync(m => m.Id == id);

        public async Task<(List<Marks> Records, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = BaseQuery();
            var totalCount = await query.CountAsync();

            var records = await query
                .OrderByDescending(m => m.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (records, totalCount);
        }

        public async Task<List<Marks>> GetByStudentAndExamAsync(int studentId, int examId) =>
            await BaseQuery().Where(m => m.StudentId == studentId && m.ExamId == examId).ToListAsync();

        public async Task<Marks> AddAsync(Marks marks)
        {
            await _context.Marks.AddAsync(marks);
            await _context.SaveChangesAsync();
            return marks;
        }

        public async Task UpdateAsync(Marks marks)
        {
            _context.Marks.Update(marks);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Marks marks)
        {
            _context.Marks.Remove(marks);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int examId, int studentId, int subjectId) =>
            await _context.Marks.AnyAsync(m => m.ExamId == examId && m.StudentId == studentId && m.SubjectId == subjectId);
    }

}

