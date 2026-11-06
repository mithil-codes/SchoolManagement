using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class SubjectRepository:ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subject?> GetByIdAsync(int id) =>
            await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);

        public async Task<(List<Subject> Subjects, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Subjects.AsQueryable();
            var totalCount = await query.CountAsync();

            var subjects = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (subjects, totalCount);
        }

        public async Task<Subject> AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subject subject)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }
}

