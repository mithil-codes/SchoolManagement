using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.ClassSubject
{
    public class CreateClassSubjectDto
    {
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public string? TeacherId { get; set; }
    }
}
