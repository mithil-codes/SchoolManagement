using SchoolManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.AttendaceDto
{
    public class CreateAttendanceDto
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Remarks { get; set; }
        public string? MarkedByTeacherId { get; set; }
    }
}
