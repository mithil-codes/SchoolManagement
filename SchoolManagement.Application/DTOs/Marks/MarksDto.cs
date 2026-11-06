using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Marks
{
    public class MarksDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public double ObtainedMarks { get; set; }
        public double TotalMarks { get; set; }
        public double Percentage { get; set; }
        public string Grade { get; set; } = string.Empty;
    }
}
