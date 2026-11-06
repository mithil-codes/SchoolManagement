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
    public class ClassRepository:IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Classes
    .Include(c => c.ClassTeacher)
    .FirstOrDefaultAsync(c => c.Id == id);
        
        }

        public async Task<(List<Class> Classes, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
         

            var query = _context.Classes.Include(c => c.ClassTeacher).AsQueryable();
            var totalCount = await query.CountAsync();

            var classes = await query
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (classes, totalCount);
        }

        public async Task<Class> AddAsync(Class classEntity)
        {
            await _context.Classes.AddAsync(classEntity);
            await _context.SaveChangesAsync();
            return classEntity;
        }

        public async Task UpdateAsync(Class classEntity)
        {
            _context.Classes.Update(classEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Class classEntity)
        {
            _context.Classes.Remove(classEntity);
            await _context.SaveChangesAsync();
        }
    }
}

