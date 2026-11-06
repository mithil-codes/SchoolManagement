using AutoMapper;
using SchoolManagement.Application.DTOs.Marks;
using SchoolManagement.Application.Helpers;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Mappings
{
    public class MarksProfile:Profile
    {
        public MarksProfile()
        {
            CreateMap<Marks, MarksDto>()
           .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.ExamName))
           .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
           .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
           .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src =>
               src.TotalMarks == 0 ? 0 : Math.Round((src.ObtainedMarks / src.TotalMarks) * 100, 2)))
           .ForMember(dest => dest.Grade, opt => opt.MapFrom(src =>
               GradeCalculator.CalculateGrade(src.TotalMarks == 0 ? 0 : (src.ObtainedMarks / src.TotalMarks) * 100)));

            CreateMap<CreateMarksDto, Marks>();
        }

    }
}
