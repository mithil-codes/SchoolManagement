using AutoMapper;
using SchoolManagement.Application.DTOs.Student;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class StudentProfile:Profile
    {

        public StudentProfile()
        {
            // Entity → StudentDto (Response ke liye)
            CreateMap<Student, StudentDto>()
                    .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
                         .ForMember(dest => dest.HasLoginAccount, opt => opt.MapFrom(src => src.ApplicationUserId != null));

            // CreateStudentDto → Entity (naya student banate waqt)
            CreateMap<CreateStudentDto, Student>();

            // UpdateStudentDto → Entity (update karte waqt)
            CreateMap<UpdateStudentDto, Student>();
        }
      
    }
}
