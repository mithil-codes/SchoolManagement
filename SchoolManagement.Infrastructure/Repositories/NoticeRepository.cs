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
    public class NoticeRepository:INoticeRepository
    {
        private readonly ApplicationDbContext _context;

        public NoticeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<Notice> BaseQuery() => _context.Notices.Include(n => n.PostedByUser);

        public async Task<Notice?> GetByIdAsync(int id) =>
            await BaseQuery().FirstOrDefaultAsync(n => n.Id == id);

        public async Task<(List<Notice> Notices, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = BaseQuery();
            var totalCount = await query.CountAsync();

            var notices = await query
                .OrderByDescending(n => n.PublishDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (notices, totalCount);
        }

        public async Task<List<Notice>> GetActiveNoticesAsync() =>
            await BaseQuery()
                .Where(n => n.IsActive && (n.ExpiryDate == null || n.ExpiryDate >= DateTime.UtcNow))
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();

        public async Task<Notice> AddAsync(Notice notice)
        {
            await _context.Notices.AddAsync(notice);
            await _context.SaveChangesAsync();
            return notice;
        }

        public async Task UpdateAsync(Notice notice)
        {
            _context.Notices.Update(notice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Notice notice)
        {
            _context.Notices.Remove(notice);
            await _context.SaveChangesAsync();
        }
    }
}
