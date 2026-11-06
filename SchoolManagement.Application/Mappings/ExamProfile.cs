using AutoMapper;
using SchoolManagement.Application.DTOs.Exam;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class ExamProfile:Profile
    {
        public ExamProfile()
        {
            CreateMap<Exam, ExamDto>()
               .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName));

            CreateMap<CreateExamDto, Exam>();
            CreateMap<UpdateExamDto, Exam>();
        }
       
    }
}
