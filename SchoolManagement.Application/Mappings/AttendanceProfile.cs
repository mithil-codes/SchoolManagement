using AutoMapper;
using SchoolManagement.Application.DTOs.AttendaceDto;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class AttendanceProfile:Profile
    {
        public AttendanceProfile()
        {
            CreateMap<Attendance, AttendanceDto>()
               .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
               .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
               .ForMember(dest => dest.MarkedByTeacherName,
                   opt => opt.MapFrom(src => src.MarkedByTeacher != null
                       ? src.MarkedByTeacher.FirstName + " " + src.MarkedByTeacher.LastName
                       : null));

            CreateMap<CreateAttendanceDto, Attendance>();
        }
    }
       
}

