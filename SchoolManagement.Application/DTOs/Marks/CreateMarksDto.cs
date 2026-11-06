using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Marks
{
    public class CreateMarksDto
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public double ObtainedMarks { get; set; }
        public double TotalMarks { get; set; }
    }
}
