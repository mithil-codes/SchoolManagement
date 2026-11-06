using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using SchoolManagement.Application.DTOs.Class;
using SchoolManagement.Application.DTOs.Common;
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
    public class ClassService:IClassService
    {
        private readonly IClassRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
       // private readonly UserManager<ApplicationUser> _userManager;

        public ClassService(IClassRepository repository, IMapper mapper, IMemoryCache cache )
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
           
        }

        //private async Task<ClassDto> MapWithTeacherName(Class classEntity)
        //{
        //    var dto = _mapper.Map<ClassDto>(classEntity);

        //    if (!string.IsNullOrEmpty(classEntity.ClassTeacherId))
        //    {
        //        var teacherUser = await _userManager.FindByIdAsync(classEntity.ClassTeacherId);
        //        dto.ClassTeacherName = teacherUser != null ? $"{teacherUser.FirstName} {teacherUser.LastName}" : null;
        //    }

        //    return dto;
        //}

        public async Task<ClassDto?> GetByIdAsync(int id)
        {
            string cacheKey = $"class_{id}";
            if (_cache.TryGetValue(cacheKey, out ClassDto? cached)) return cached;

            var classEntity = await _repository.GetByIdAsync(id);
            if (classEntity == null) return null;

            var dto = _mapper.Map<ClassDto>(classEntity);   // simple direct mapping
            _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));
            return dto;
            //string cacheKey = $"class_{id}";
            //if (_cache.TryGetValue(cacheKey, out ClassDto? cached)) return cached;

            //var classEntity = await _repository.GetByIdAsync(id);
            //if (classEntity == null) return null;

            //var dto = await MapWithTeacherName(classEntity);
            //_cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));
            //return dto;
        }

        public async Task<PagedResultDto<ClassDto>> GetAllAsync(PaginationParams pagination)
        {
            var (classes, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);

            var dtos = new List<ClassDto>();
            foreach (var c in classes)
            {
                dtos.Add(_mapper.Map<ClassDto>(c));
                //dtos.Add(await MapWithTeacherName(c));
            }

            return new PagedResultDto<ClassDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<ClassDto> CreateAsync(CreateClassDto dto)
        {
            var classEntity = _mapper.Map<Class>(dto);

            var created = await _repository.AddAsync(classEntity);

            return _mapper.Map<ClassDto>(created);

            //return _mapper.Map<ClassDto>(created);
            //var classEntity = _mapper.Map<Class>(dto);
            //var created = await _repository.AddAsync(classEntity);
            //return await MapWithTeacherName(created);
        }

        public async Task<bool> UpdateAsync(int id, UpdateClassDto dto)
        {
            var classEntity = await _repository.GetByIdAsync(id);
            if (classEntity == null) return false;

            _mapper.Map(dto, classEntity);
            await _repository.UpdateAsync(classEntity);
            _cache.Remove($"class_{id}");
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var classEntity = await _repository.GetByIdAsync(id);
            if (classEntity == null) return false;

            await _repository.DeleteAsync(classEntity);
            _cache.Remove($"class_{id}");
            return true;
        }
    }
}
