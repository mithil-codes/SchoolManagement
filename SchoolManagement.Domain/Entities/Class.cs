using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string AcademicYear { get; set; } = string.Empty;

        // FK → ApplicationUser (Identity), isliye string hoga (Identity ka Id string hota hai)
        public string? ClassTeacherId { get; set; }
        public ApplicationUser? ClassTeacher { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();
    }
}
