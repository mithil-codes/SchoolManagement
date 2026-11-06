using AutoMapper;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Notice;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services
{
    public class NoticeService:INoticeService
    {
        private readonly INoticeRepository _repository;
        private readonly IMapper _mapper;

        public NoticeService(INoticeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NoticeDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<NoticeDto>(entity);
        }

        public async Task<PagedResultDto<NoticeDto>> GetAllAsync(PaginationParams pagination)
        {
            var (notices, totalCount) = await _repository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            var dtos = _mapper.Map<List<NoticeDto>>(notices);

            return new PagedResultDto<NoticeDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<List<NoticeDto>> GetActiveNoticesAsync()
        {
            var notices = await _repository.GetActiveNoticesAsync();
            return _mapper.Map<List<NoticeDto>>(notices);
        }

        public async Task<NoticeDto> CreateAsync(CreateNoticeDto dto, string postedByUserId)
        {
            var notice = _mapper.Map<Notice>(dto);
            notice.PostedByUserId = postedByUserId;   // token se aaya hua user id yahan set ho raha hai
            notice.PublishDate = DateTime.UtcNow;
            notice.IsActive = true;

            var created = await _repository.AddAsync(notice);
            var fullEntity = await _repository.GetByIdAsync(created.Id);
            return _mapper.Map<NoticeDto>(fullEntity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateNoticeDto dto)
        {
            var notice = await _repository.GetByIdAsync(id);
            if (notice == null) return false;

            _mapper.Map(dto, notice);
            await _repository.UpdateAsync(notice);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notice = await _repository.GetByIdAsync(id);
            if (notice == null) return false;

            await _repository.DeleteAsync(notice);
            return true;
        }
    }
}
