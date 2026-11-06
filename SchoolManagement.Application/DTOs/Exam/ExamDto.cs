using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Exam
{
    public class ExamDto
    {
        public int Id { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
    }
}
