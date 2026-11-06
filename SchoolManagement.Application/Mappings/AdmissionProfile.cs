using AutoMapper;
using SchoolManagement.Application.DTOs.Admission;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class AdmissionProfile:Profile
    {
        public AdmissionProfile()
        {
            CreateMap<AdmissionApplication, ApplicationDto>()
               .ForMember(dest => dest.DesiredClassName, opt => opt.MapFrom(src => src.DesiredClass.ClassName))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<SubmitApplicationDto, AdmissionApplication>();
        }
       
    }
}
