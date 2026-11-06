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
    public class ClassSubjectRepository:IClassSubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassSubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClassSubject?> GetByIdAsync(int id)
        {
            return await _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                    .Include(cs => cs.Teacher)     
                .FirstOrDefaultAsync(cs => cs.Id == id);
        }

        public async Task<(List<ClassSubject> ClassSubjects, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.ClassSubjects
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                .Include(cs => cs.Teacher)
                .AsQueryable();
            var totalCount = await query.CountAsync();

            var classSubjects = await query
                .OrderBy(cs => cs.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (classSubjects, totalCount);
        }

        public async Task<ClassSubject> AddAsync(ClassSubject classSubject)
        {
            await _context.ClassSubjects.AddAsync(classSubject);
            await _context.SaveChangesAsync();
            return classSubject;
        }

        public async Task UpdateAsync(ClassSubject classSubject)
        {
            _context.ClassSubjects.Update(classSubject);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClassSubject classSubject)
        {
            _context.ClassSubjects.Remove(classSubject);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ClassSubject>> GetByTeacherIdAsync(string teacherId)
        {
            return await _context.ClassSubjects   // yahan _context use karna SAHI hai, kyunki ye Repository hai
                .Include(cs => cs.Class)
                .Include(cs => cs.Subject)
                .Where(cs => cs.TeacherId == teacherId)
                .ToListAsync();
        }
    }
}

