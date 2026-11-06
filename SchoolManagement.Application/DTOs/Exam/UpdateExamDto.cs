using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Exam
{
    public class UpdateExamDto
    {
        public string ExamName { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
    }
}
