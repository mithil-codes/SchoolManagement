using AutoMapper;
using SchoolManagement.Application.DTOs.Notice;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class NoticeProfile:Profile
    {
        public NoticeProfile()
        {
            CreateMap<Notice, NoticeDto>()
               .ForMember(dest => dest.PostedByName, opt => opt.MapFrom(src =>
                   src.PostedByUser != null ? src.PostedByUser.FirstName + " " + src.PostedByUser.LastName : null));

            CreateMap<CreateNoticeDto, Notice>();
            CreateMap<UpdateNoticeDto, Notice>();
        }
    }
}
