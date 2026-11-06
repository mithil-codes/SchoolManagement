using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Exam;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class ExamService:IExamService
    {
        private readonly IExamRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ExamService(IExamRepository repository, IMapper mapper, IMemoryCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<ExamDto?> GetByIdAsync(int id)
        {
            string cacheKey = $"exam_{id}";
            if (_cache.TryGetValue(cacheKey, out ExamDto? cached)) return cached;

            var exam = await _repository.GetByIdAsync(id);
            if (exam == null) return null;

            var dto = _mapper.Map<ExamDto>(exam);
            _cache.Set(cacheKey, dto, TimeSpan.FromMinutes(5));
            return dto;
        }

        public async Task<PagedResultDto<ExamDto>> GetAllAsync(PaginationParams pagination)
        {
            var (exams, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            var dtos = _mapper.Map<List<ExamDto>>(exams);

            return new PagedResultDto<ExamDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<ExamDto> CreateAsync(CreateExamDto dto)
        {
            var exam = _mapper.Map<Exam>(dto);
            var created = await _repository.AddAsync(exam);

            var fullExam = await _repository.GetByIdAsync(created.Id);
            return _mapper.Map<ExamDto>(fullExam);
        }

        public async Task<bool> UpdateAsync(int id, UpdateExamDto dto)
        {
            var exam = await _repository.GetByIdAsync(id);
            if (exam == null) return false;

            _mapper.Map(dto, exam);
            await _repository.UpdateAsync(exam);
            _cache.Remove($"exam_{id}");
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exam = await _repository.GetByIdAsync(id);
            if (exam == null) return false;

            await _repository.DeleteAsync(exam);
            _cache.Remove($"exam_{id}");
            return true;
        }
    }
}

