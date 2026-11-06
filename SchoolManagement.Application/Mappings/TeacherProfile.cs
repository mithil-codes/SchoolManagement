using AutoMapper;
using SchoolManagement.Application.DTOs.Teacher;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class TeacherProfile:Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDto>()
            .ForMember(dest => dest.HasLoginAccount, opt => opt.MapFrom(src => src.ApplicationUserId != null));
            CreateMap<CreateTeacherDto, Teacher>();
            CreateMap<UpdateTeacherDto, Teacher>();
        }
    }
}
