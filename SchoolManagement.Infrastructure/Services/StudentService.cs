using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class  StudentService : IStudentService
    {

        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StudentService(IStudentRepository repository, IMapper mapper, IMemoryCache cache, UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            string cacheKey = $"student_{id}";

            if (_cache.TryGetValue(cacheKey, out StudentDto? cachedStudent))
            {
                return cachedStudent;   // cache me mil gaya, DB call ki zaroorat nahi
            }

            var student = await _repository.GetByIdAsync(id);
            if (student == null) return null;

            var dto = _mapper.Map<StudentDto>(student);

            _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));   // 5 min ke liye cache karo

            return dto;
        }

        public async Task<PagedResultDto<StudentDto>> GetAllAsync(PaginationParams pagination)
        {
            var (students, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);

            var dtos = _mapper.Map<List<StudentDto>>(students);

            return new PagedResultDto<StudentDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
        {
            var highest = await _repository.GetHighestAdmissionNumberAsync();

            var student = _mapper.Map<Student>(dto);
            student.AdmissionNumber = $"S{(highest + 1):D3}";
            student.CreatedAt = DateTime.UtcNow;
            student.IsActive = true;

            var created = await _repository.AddAsync(student);
            return _mapper.Map<StudentDto>(created);
        }

        public async Task<bool> UpdateAsync(int id, UpdateStudentDto dto)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null) return false;

            _mapper.Map(dto, student);   // existing entity ke upar dto ki values overwrite karo
            await _repository.UpdateAsync(student);

            _cache.Remove($"student_{id}");   // update hua to purana cache hata do
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student == null) return false;

            await _repository.DeleteAsync(student);
            _cache.Remove($"student_{id}");
            return true;
        }

        public async Task<(bool Success, string Message)> CreateLoginAsync(int studentId, CreateLoginDto dto)
        {
            var student = await _repository.GetByIdAsync(studentId);
            if (student == null)
            {
                return (false, "Student not found");
            }

            if (!string.IsNullOrEmpty(student.ApplicationUserId))
            {
                return (false, "This student already has a login account");
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return (false, "Email already registered with another account");
            }

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = student.FirstName,
                LastName = student.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, errors);
            }

            // "Student" role hamesha fixed hai yahan — koi choice nahi
            var roleExists = await _roleManager.RoleExistsAsync("Student");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Student"));
            }
            await _userManager.AddToRoleAsync(user, "Student");

            // Link banao — Student record ko is naye user se jodo
            student.ApplicationUserId = user.Id;
            await _repository.UpdateAsync(student);

            _cache.Remove($"student_{studentId}");

            return (true, "Login created successfully for this student");
        }
        public async Task<StudentDto?> GetByApplicationUserIdAsync(string applicationUserId)
        {
            var student = await _repository.GetByApplicationUserIdAsync(applicationUserId);
            return student == null ? null : _mapper.Map<StudentDto>(student);
        }
    }
   }


