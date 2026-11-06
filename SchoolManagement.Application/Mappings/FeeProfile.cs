using AutoMapper;
using SchoolManagement.Application.DTOs.Fee;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class FeeProfile:Profile
    {
        public FeeProfile()
        {
            CreateMap<Fee, FeeDto>()
               .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
               .ForMember(dest => dest.DueAmount, opt => opt.MapFrom(src => src.TotalAmount - src.PaidAmount))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreateFeeDto, Fee>();
        }
    }
}
