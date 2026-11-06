using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            return await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<(List<Teacher> Teachers, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Teachers.AsQueryable();
            var totalCount = await query.CountAsync();

            var teachers = await query
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (teachers, totalCount);
        }

        public async Task<Teacher> AddAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }
        public async Task<Teacher?> GetByApplicationUserIdAsync(string applicationUserId)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(t => t.ApplicationUserId == applicationUserId);
        }
    }
}