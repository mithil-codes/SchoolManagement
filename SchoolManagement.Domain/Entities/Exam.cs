using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        public string ExamName { get; set; } = string.Empty;   // "Mid Term", "Final Term"
        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
        public DateTime ExamDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty;

        public ICollection<Marks> Marks { get; set; } = new List<Marks>();
    }
}
