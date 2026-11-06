using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Domain.Identity;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Application.DTOs.ClassSubject;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly IClassSubjectRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClassSubjectService(IClassSubjectRepository repository, IMapper mapper, IMemoryCache cache, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
            _userManager = userManager;
        }

        private async Task<ClassSubjectDto> MapWithTeacherName(ClassSubject entity)
        {
            var dto = _mapper.Map<ClassSubjectDto>(entity);

            if (!string.IsNullOrEmpty(entity.TeacherId))
            {
                var teacherUser = await _userManager.FindByIdAsync(entity.TeacherId);
                dto.TeacherName = teacherUser != null ? $"{teacherUser.FirstName} {teacherUser.LastName}" : null;
            }

            return dto;
        }

        public async Task<ClassSubjectDto?> GetByIdAsync(int id)
        {
            string cacheKey = $"classsubject_{id}";
            if (_cache.TryGetValue(cacheKey, out ClassSubjectDto? cached)) return cached;

            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            var dto = await MapWithTeacherName(entity);
            _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));
            return dto;
        }

        public async Task<PagedResultDto<ClassSubjectDto>> GetAllAsync(PaginationParams pagination)
        {
            var (entities, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);

            var dtos = new List<ClassSubjectDto>();
            foreach (var e in entities)
            {
                dtos.Add(await MapWithTeacherName(e));
            }

            return new PagedResultDto<ClassSubjectDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<ClassSubjectDto> CreateAsync(CreateClassSubjectDto dto)
        {
            var entity = _mapper.Map<ClassSubject>(dto);
            var created = await _repository.AddAsync(entity);

            // Class/Subject navigation properties abhi load nahi hain naye create ke baad, dobara fetch karte hain
            var fullEntity = await _repository.GetByIdAsync(created.Id);
            return await MapWithTeacherName(fullEntity!);
        }

        public async Task<bool> UpdateAsync(int id, UpdateClassSubjectDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity);
            _cache.Remove($"classsubject_{id}");
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            _cache.Remove($"classsubject_{id}");
            return true;
        }
    }
}

