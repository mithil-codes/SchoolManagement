using AutoMapper;
using SchoolManagement.Application.DTOs.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Entities;
namespace SchoolManagement.Application.Mappings
{
    public class SubjectProfile:Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectDto>();
            CreateMap<CreateSubjectDto, Subject>();
            CreateMap<UpdateSubjectDto, Subject>();
        }
    }
}
