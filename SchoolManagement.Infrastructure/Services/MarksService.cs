using AutoMapper;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Marks;
using SchoolManagement.Application.Helpers;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
   public class MarksService:IMarksService
    {
        private readonly IMarksRepository _repository;
        private readonly IMapper _mapper;

        public MarksService(IMarksRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MarksDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<MarksDto>(entity);
        }

        public async Task<PagedResultDto<MarksDto>> GetAllAsync(PaginationParams pagination)
        {
            var (records, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            var dtos = _mapper.Map<List<MarksDto>>(records);

            return new PagedResultDto<MarksDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<StudentReportCardDto?> GetReportCardAsync(int studentId, int examId)
        {
            var records = await _repository.GetByStudentAndExamAsync(studentId, examId);
            if (!records.Any()) return null;

            var subjectMarks = _mapper.Map<List<MarksDto>>(records);

            double totalObtained = records.Sum(r => r.ObtainedMarks);
            double totalMaximum = records.Sum(r => r.TotalMarks);
            double overallPercentage = totalMaximum == 0 ? 0 : Math.Round((totalObtained / totalMaximum) * 100, 2);

            return new StudentReportCardDto
            {
                StudentId = studentId,
                StudentName = records.First().Student.FirstName + " " + records.First().Student.LastName,
                ExamName = records.First().Exam.ExamName,
                Subjects = subjectMarks,
                TotalObtained = totalObtained,
                TotalMaximum = totalMaximum,
                OverallPercentage = overallPercentage,
                OverallGrade = GradeCalculator.CalculateGrade(overallPercentage)
            };
        }

        public async Task<(bool Success, string Message, MarksDto? Data)> CreateAsync(CreateMarksDto dto)
        {
            var exists = await _repository.ExistsAsync(dto.ExamId, dto.StudentId, dto.SubjectId);
            if (exists)
            {
                return (false, "Marks already entered for this student in this subject for this exam", null);
            }

            if (dto.ObtainedMarks > dto.TotalMarks)
            {
                return (false, "Obtained marks cannot be greater than total marks", null);
            }

            var entity = _mapper.Map<Marks>(dto);
            var created = await _repository.AddAsync(entity);

            var fullEntity = await _repository.GetByIdAsync(created.Id);
            return (true, "Marks added successfully", _mapper.Map<MarksDto>(fullEntity));
        }

        public async Task<bool> UpdateAsync(int id, UpdateMarksDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.ObtainedMarks = dto.ObtainedMarks;
            entity.TotalMarks = dto.TotalMarks;

            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}

