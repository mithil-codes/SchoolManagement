using SchoolManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.AttendaceDto
{
    public class UpdateAttendanceDto
    {
        public AttendanceStatus Status { get; set; }
        public string? Remarks { get; set; }
    }
}
