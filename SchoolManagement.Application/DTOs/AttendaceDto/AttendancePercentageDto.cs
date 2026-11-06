using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.AttendaceDto
{
    public class AttendancePercentageDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int TotalDays { get; set; }
        public int PresentDays { get; set; }
        public double AttendancePercentage { get; set; }
    }
}
