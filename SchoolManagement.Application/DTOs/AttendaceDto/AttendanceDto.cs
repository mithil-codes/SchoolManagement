using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.AttendaceDto
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Remarks { get; set; }
        public string? MarkedByTeacherId { get; set; }
        public string? MarkedByTeacherName { get; set; }
       
    }
}