using AutoMapper;
using SchoolManagement.Application.DTOs.AttendaceDto;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class AttendanceService:IAttendanceService
    {


        private readonly IAttendanceRepository _repository;
        private readonly IMapper _mapper;

        public AttendanceService(IAttendanceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<AttendanceDto>(entity);
        }

        public async Task<PagedResultDto<AttendanceDto>> GetAllAsync(PaginationParams pagination)
        {
            var (records, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            var dtos = _mapper.Map<List<AttendanceDto>>(records);

            return new PagedResultDto<AttendanceDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<List<AttendanceDto>> GetByClassAndDateAsync(int classId, DateTime date)
        {
            var records = await _repository.GetByClassAndDateAsync(classId, date);
            return _mapper.Map<List<AttendanceDto>>(records);
        }

        public async Task<AttendancePercentageDto> GetAttendancePercentageAsync(int studentId)
        {
            var records = await _repository.GetByStudentIdAsync(studentId);

            int totalDays = records.Count;
            int presentDays = records.Count(r => r.Status == AttendanceStatus.Present);
            double percentage = totalDays == 0 ? 0 : Math.Round((presentDays * 100.0) / totalDays, 2);

            return new AttendancePercentageDto
            {
                StudentId = studentId,
                StudentName = records.FirstOrDefault()?.Student.FirstName + " " + records.FirstOrDefault()?.Student.LastName,
                TotalDays = totalDays,
                PresentDays = presentDays,
                AttendancePercentage = percentage
            };
        }

        public async Task<(bool Success, string Message, AttendanceDto? Data)> CreateAsync(CreateAttendanceDto dto)
        {
            // Duplicate check — ek student ki ek din ki attendance dobara na bane
            var exists = await _repository.ExistsForStudentOnDateAsync(dto.StudentId, dto.AttendanceDate);
            if (exists)
            {
                return (false, "Attendance already marked for this student on this date", null);
            }

            var entity = _mapper.Map<Attendance>(dto);
            var created = await _repository.AddAsync(entity);

            var fullEntity = await _repository.GetByIdAsync(created.Id);
            var resultDto = _mapper.Map<AttendanceDto>(fullEntity);

            return (true, "Attendance marked successfully", resultDto);
        }

        public async Task<bool> UpdateAsync(int id, UpdateAttendanceDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Status = dto.Status;
            entity.Remarks = dto.Remarks;

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


