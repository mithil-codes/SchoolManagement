using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SchoolManagement.Application.DTOs.Admission;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Infrastructure.Services
{
    public class AdmissionService : IAdmissionService
    {
        private readonly IAdmissionRepository _repository;
     //   private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public AdmissionService(
            IAdmissionRepository repository,
            IStudentRepository studentRepository,
            IStudentService studentService,
            IMapper mapper)
        {
            _repository = repository;
            //_studentRepository = studentRepository;
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<ApplicationDto> SubmitApplicationAsync(SubmitApplicationDto dto)
        {
            var application = _mapper.Map<AdmissionApplication>(dto);
            application.Status = ApplicationStatus.Pending;
            application.AppliedDate = DateTime.UtcNow;

            var created = await _repository.AddAsync(application);
            var fullEntity = await _repository.GetByIdAsync(created.Id);
            return _mapper.Map<ApplicationDto>(fullEntity);
        }

        public async Task<PagedResultDto<ApplicationDto>> GetAllAsync(PaginationParams pagination)
        {
            var (applications, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            var dtos = _mapper.Map<List<ApplicationDto>>(applications);

            return new PagedResultDto<ApplicationDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<(bool Success, string Message)> ApproveAsync(int id)
        {
            var application = await _repository.GetByIdAsync(id);
            if (application == null) return (false, "Application not found");

            if (application.Status != ApplicationStatus.Pending)
                return (false, "This application has already been processed");

            // Application ka data Student ke DTO me copy karo (AdmissionNumber nahi, wo StudentService banayega)
            var createStudentDto = new CreateStudentDto
            {
                FirstName = application.FirstName,
                LastName = application.LastName,
                DateOfBirth = application.DateOfBirth,
                Gender = application.Gender,
                Email = application.Email,
                Phone = application.Phone,
                Address = application.Address,
                ClassId = application.DesiredClassId
            };

            // Student ban gaya, AdmissionNumber andar se auto-generate hua
            var newStudent = await _studentService.CreateAsync(createStudentDto);

            // Application ko Approved mark karo aur naye Student se link karo
            application.Status = ApplicationStatus.Approved;
            application.CreatedStudentId = newStudent.Id;
            await _repository.UpdateAsync(application);

            return (true, $"Application approved. Student created with Admission Number {newStudent.AdmissionNumber}");
        }

        public async Task<(bool Success, string Message)> RejectAsync(int id)
        {
            var application = await _repository.GetByIdAsync(id);
            if (application == null) return (false, "Application not found");

            if (application.Status != ApplicationStatus.Pending)
                return (false, "This application has already been processed");

            application.Status = ApplicationStatus.Rejected;
            await _repository.UpdateAsync(application);

            return (true, "Application rejected");
        }
    }
}
