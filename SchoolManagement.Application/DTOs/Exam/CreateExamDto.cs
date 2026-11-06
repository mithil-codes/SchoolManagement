using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Exam
{
    public class CreateExamDto
    {
        public string ExamName { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public DateTime ExamDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
    }
}
