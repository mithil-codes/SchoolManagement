using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Marks

    {
        public int Id { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; } = null!;

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public double ObtainedMarks { get; set; }
        public double TotalMarks { get; set; }

        public string Grade { get; set; } = string.Empty;   // A+, A, B, etc. — calculate karenge
    }
}



