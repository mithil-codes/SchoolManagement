using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Marks
{
    public class StudentReportCardDto
    {

        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string ExamName { get; set; } = string.Empty;
        public List<MarksDto> Subjects { get; set; } = new();
        public double TotalObtained { get; set; }
        public double TotalMaximum { get; set; }
        public double OverallPercentage { get; set; }
        public string OverallGrade { get; set; } = string.Empty;
    }
}
