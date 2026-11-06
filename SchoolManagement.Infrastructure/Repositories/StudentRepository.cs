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
    public class StudentRepository:IStudentRepository
    {
        
    
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Class)          // Class ka data bhi saath me load karo (join)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<(List<Student> Students, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = _context.Students.Include(s => s.Class).AsQueryable();

            var totalCount = await query.CountAsync();

            var students = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)     // paging logic
                .Take(pageSize)
                .ToListAsync();

            return (students, totalCount);
        }

        public async Task<Student> AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(s => s.Id == id);
        }
        public async Task<Student?> GetByApplicationUserIdAsync(string applicationUserId)
        {
            return await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == applicationUserId);
        }
        public async Task<int> GetHighestAdmissionNumberAsync()
        {
            var admissionNumbers = await _context.Students
                .Select(s => s.AdmissionNumber)
                .ToListAsync();

            int highest = 0;
            foreach (var admNo in admissionNumbers)
            {
                var numericPart = admNo?.Replace("S", "");
                if (int.TryParse(numericPart, out int num) && num > highest)
                {
                    highest = num;
                }
            }
            return highest;
        }
    }
}
