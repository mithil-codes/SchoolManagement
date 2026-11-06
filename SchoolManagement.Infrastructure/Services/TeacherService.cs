using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using SchoolManagement.Application.DTOs.ClassSubject;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Application.DTOs.Teacher;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Identity;
using SchoolManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
  
        public class TeacherService : ITeacherService
        {
            private readonly ITeacherRepository _repository;
            private readonly IMapper _mapper;
            private readonly IMemoryCache _cache;
          private readonly IClassSubjectRepository _classSubjectRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TeacherService(ITeacherRepository repository, IMapper mapper, IMemoryCache cache,
                UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager, IClassSubjectRepository classSubjectRepository)
            {
                _repository = repository;
                _mapper = mapper;
                _cache = cache;
            _userManager = userManager;
            _roleManager = roleManager;
            roleManager= roleManager;
        }

            public async Task<TeacherDto?> GetByIdAsync(int id)
            {
                string cacheKey = $"teacher_{id}";
                if (_cache.TryGetValue(cacheKey, out TeacherDto? cached)) return cached;

                var teacher = await _repository.GetByIdAsync(id);
                if (teacher == null) return null;

                var dto = _mapper.Map<TeacherDto>(teacher);
                _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));
                return dto;
            }

            public async Task<PagedResultDto<TeacherDto>> GetAllAsync(PaginationParams pagination)
            {
                var (teachers, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
                var dtos = _mapper.Map<List<TeacherDto>>(teachers);

                return new PagedResultDto<TeacherDto>
                {
                    Items = dtos,
                    TotalCount = totalCount,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize
                };
            }

            public async Task<TeacherDto> CreateAsync(CreateTeacherDto dto)
            {
                var teacher = _mapper.Map<Teacher>(dto);
                teacher.IsActive = true;

                var created = await _repository.AddAsync(teacher);
                return _mapper.Map<TeacherDto>(created);
            }

            public async Task<bool> UpdateAsync(int id, UpdateTeacherDto dto)
            {
                var teacher = await _repository.GetByIdAsync(id);
                if (teacher == null) return false;

                _mapper.Map(dto, teacher);
                await _repository.UpdateAsync(teacher);
                _cache.Remove($"teacher_{id}");
                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var teacher = await _repository.GetByIdAsync(id);
                if (teacher == null) return false;

                await _repository.DeleteAsync(teacher);
                _cache.Remove($"teacher_{id}");
                return true;
            }
        public async Task<(bool Success, string Message)> CreateLoginAsync(int teacherId, CreateLoginDto dto)
        {
            var teacher = await _repository.GetByIdAsync(teacherId);
            if (teacher == null)
            {
                return (false, "Teacher not found");
            }

            if (!string.IsNullOrEmpty(teacher.ApplicationUserId))
            {
                return (false, "This teacher already has a login account");
            }

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return (false, "Email already registered with another account");
            }

            // Teacher.Name ek hi field hai (FirstName/LastName alag nahi), to split karte hain
            var nameParts = teacher.Name.Split(' ', 2);
            var firstName = nameParts.Length > 0 ? nameParts[0] : teacher.Name;
            var lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = firstName,
                LastName = lastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, errors);
            }

            var roleExists = await _roleManager.RoleExistsAsync("Teacher");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole("Teacher"));
            }
            await _userManager.AddToRoleAsync(user, "Teacher");

            teacher.ApplicationUserId = user.Id;
            await _repository.UpdateAsync(teacher);

            _cache.Remove($"teacher_{teacherId}");

            return (true, "Login created successfully for this teacher");
        }

        public async Task<TeacherDto?> GetByApplicationUserIdAsync(string applicationUserId)
        {
            var teacher = await _repository.GetByApplicationUserIdAsync(applicationUserId);
            return teacher == null ? null : _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<List<ClassSubjectDto>> GetMyClassSubjectsAsync(string applicationUserId)
        {
            var myClassSubjects = await _classSubjectRepository.GetByTeacherIdAsync(applicationUserId);
            return _mapper.Map<List<ClassSubjectDto>>(myClassSubjects);
        }


    }

}


