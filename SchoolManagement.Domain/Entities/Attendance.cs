using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;
        public DateTime AttendanceDate { get; set; }
        public AttendanceStatus Status { get; set; }

        public string? Remarks { get; set; }
        public string? MarkedByTeacherId { get; set; }   // ApplicationUser FK
        public ApplicationUser? MarkedByTeacher { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
