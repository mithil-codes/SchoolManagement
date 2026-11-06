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
    public class ExamRepository:IExamRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Exam?> GetByIdAsync(int id) =>
            await _context.Exams.Include(e => e.Class).FirstOrDefaultAsync(e => e.Id == id);

        public async Task<(List<Exam> Exams, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Exams.Include(e => e.Class).AsQueryable();
            var totalCount = await query.CountAsync();

            var exams = await query
                .OrderByDescending(e => e.ExamDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (exams, totalCount);
        }

        public async Task<Exam> AddAsync(Exam exam)
        {
            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task UpdateAsync(Exam exam)
        {
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Exam exam)
        {
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
        }
    }
}

