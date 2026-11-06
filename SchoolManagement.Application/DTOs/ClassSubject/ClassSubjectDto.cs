using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.ClassSubject
{
    public class ClassSubjectDto
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;

        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;

        public string? TeacherId { get; set; }
        public string? TeacherName { get; set; }
    }
}
