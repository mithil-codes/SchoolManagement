using AutoMapper;
using SchoolManagement.Application.DTOs.Class;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class ClassProfile:Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassDto>()
                 .ForMember(dest => dest.ClassTeacherName,
        opt => opt.MapFrom(src => src.ClassTeacher != null
            ? src.ClassTeacher.FirstName + " " + src.ClassTeacher.LastName
            : null));

            CreateMap<CreateClassDto, Class>();
            CreateMap<UpdateClassDto, Class>();
        }
    }
}
