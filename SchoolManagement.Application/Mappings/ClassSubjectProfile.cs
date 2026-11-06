using AutoMapper;
using SchoolManagement.Application.DTOs.ClassSubject;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class ClassSubjectProfile : Profile
    {
        public ClassSubjectProfile()
        {
            CreateMap<ClassSubject, ClassSubjectDto>()
        .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
    .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
    .ForMember(dest => dest.TeacherName,
        opt => opt.MapFrom(src => src.Teacher != null
            ? src.Teacher.FirstName + " " + src.Teacher.LastName
            : null));

            CreateMap<CreateClassSubjectDto, ClassSubject>();
            CreateMap<UpdateClassSubjectDto, ClassSubject>();
        }


    }
}
