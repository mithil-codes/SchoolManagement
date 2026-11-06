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
  
        public class AdmissionRepository : IAdmissionRepository
        {
            private readonly ApplicationDbContext _context;

            public AdmissionRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<AdmissionApplication?> GetByIdAsync(int id) =>
                await _context.AdmissionApplications.Include(a => a.DesiredClass).FirstOrDefaultAsync(a => a.Id == id);

            public async Task<(List<AdmissionApplication> Applications, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
            {
                var query = _context.AdmissionApplications.Include(a => a.DesiredClass).AsQueryable();
                var totalCount = await query.CountAsync();

                var applications = await query
                    .OrderByDescending(a => a.AppliedDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (applications, totalCount);
            }

            public async Task<AdmissionApplication> AddAsync(AdmissionApplication application)
            {
                await _context.AdmissionApplications.AddAsync(application);
                await _context.SaveChangesAsync();
                return application;
            }

            public async Task UpdateAsync(AdmissionApplication application)
            {
                _context.AdmissionApplications.Update(application);
                await _context.SaveChangesAsync();
            }
        }
    }


