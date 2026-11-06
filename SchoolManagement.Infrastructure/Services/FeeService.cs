using AutoMapper;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Fee;
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
    public class FeeService:IFeeService
    {
        private readonly IFeeRepository _repository;
        private readonly IMapper _mapper;

        public FeeService(IFeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FeeDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<FeeDto>(entity);
        }

        public async Task<PagedResultDto<FeeDto>> GetAllAsync(PaginationParams pagination)
        {
            var (fees, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            var dtos = _mapper.Map<List<FeeDto>>(fees);

            return new PagedResultDto<FeeDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<List<FeeDto>> GetByStudentIdAsync(int studentId)
        {
            var fees = await _repository.GetByStudentIdAsync(studentId);
            return _mapper.Map<List<FeeDto>>(fees);
        }

        public async Task<FeeDto> CreateAsync(CreateFeeDto dto)
        {
            var fee = _mapper.Map<Fee>(dto);
            fee.Status = PaymentStatus.Pending;   // naya fee record hamesha Pending se start hoga

            var created = await _repository.AddAsync(fee);
            var fullEntity = await _repository.GetByIdAsync(created.Id);
            return _mapper.Map<FeeDto>(fullEntity);
        }

        public async Task<(bool Success, string Message, FeeDto? Data)> PayFeeAsync(int id, PayFeeDto dto)
        {
            var fee = await _repository.GetByIdAsync(id);
            if (fee == null) return (false, "Fee record not found", null);

            if (dto.AmountPaid <= 0)
                return (false, "Amount must be greater than zero", null);

            double newPaidAmount = fee.PaidAmount + dto.AmountPaid;

            if (newPaidAmount > fee.TotalAmount)
                return (false, "Paid amount exceeds total fee amount", null);

            fee.PaidAmount = newPaidAmount;
            fee.PaidDate = DateTime.UtcNow;

            // Status automatically update ho raha hai payment ke hisaab se
            if (fee.PaidAmount == fee.TotalAmount)
                fee.Status = PaymentStatus.Paid;
            else if (fee.PaidAmount > 0)
                fee.Status = PaymentStatus.PartiallyPaid;

            await _repository.UpdateAsync(fee);
            return (true, "Payment recorded successfully", _mapper.Map<FeeDto>(fee));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fee = await _repository.GetByIdAsync(id);
            if (fee == null) return false;

            await _repository.DeleteAsync(fee);
            return true;
        }
    }
}
