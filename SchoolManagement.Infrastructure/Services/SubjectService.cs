using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Subject;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{


    namespace SchoolManagement.Infrastructure.Services
    {
        public class SubjectService : ISubjectService
        {
            private readonly ISubjectRepository _repository;
            private readonly IMapper _mapper;
            private readonly IMemoryCache _cache;

            public SubjectService(ISubjectRepository repository, IMapper mapper, IMemoryCache cache)
            {
                _repository = repository;
                _mapper = mapper;
                _cache = cache;
            }

            public async Task<SubjectDto?> GetByIdAsync(int id)
            {
                string cacheKey = $"subject_{id}";
                if (_cache.TryGetValue(cacheKey, out SubjectDto? cached)) return cached;

                var subject = await _repository.GetByIdAsync(id);
                if (subject == null) return null;

                var dto = _mapper.Map<SubjectDto>(subject);
                _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));
                return dto;
            }

            public async Task<PagedResultDto<SubjectDto>> GetAllAsync(PaginationParams pagination)
            {
                var (subjects, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
                var dtos = _mapper.Map<List<SubjectDto>>(subjects);

                return new PagedResultDto<SubjectDto>
                {
                    Items = dtos,
                    TotalCount = totalCount,
                    PageNumber = pagination.PageNumber,
                    PageSize = pagination.PageSize
                };
            }

            public async Task<SubjectDto> CreateAsync(CreateSubjectDto dto)
            {
                var subject = _mapper.Map<Subject>(dto);
                var created = await _repository.AddAsync(subject);
                return _mapper.Map<SubjectDto>(created);
            }

            public async Task<bool> UpdateAsync(int id, UpdateSubjectDto dto)
            {
                var subject = await _repository.GetByIdAsync(id);
                if (subject == null) return false;

                _mapper.Map(dto, subject);
                await _repository.UpdateAsync(subject);
                _cache.Remove($"subject_{id}");
                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var subject = await _repository.GetByIdAsync(id);
                if (subject == null) return false;

                await _repository.DeleteAsync(subject);
                _cache.Remove($"subject_{id}");
                return true;
            }
        }
    }
}
