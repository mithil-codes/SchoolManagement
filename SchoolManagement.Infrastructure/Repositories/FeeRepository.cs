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
    public class FeeRepository:IFeeRepository
    {
        private readonly ApplicationDbContext _context;

        public FeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<Fee> BaseQuery() => _context.Fees.Include(f => f.Student);

        public async Task<Fee?> GetByIdAsync(int id) =>
            await BaseQuery().FirstOrDefaultAsync(f => f.Id == id);

        public async Task<(List<Fee> Fees, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = BaseQuery();
            var totalCount = await query.CountAsync();

            var fees = await query
                .OrderByDescending(f => f.DueDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (fees, totalCount);
        }

        public async Task<List<Fee>> GetByStudentIdAsync(int studentId) =>
            await BaseQuery().Where(f => f.StudentId == studentId).ToListAsync();

        public async Task<Fee> AddAsync(Fee fee)
        {
            await _context.Fees.AddAsync(fee);
            await _context.SaveChangesAsync();
            return fee;
        }

        public async Task UpdateAsync(Fee fee)
        {
            _context.Fees.Update(fee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Fee fee)
        {
            _context.Fees.Remove(fee);
            await _context.SaveChangesAsync();
        }
    }
}
