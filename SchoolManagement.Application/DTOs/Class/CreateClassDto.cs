using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Class
{
    public class CreateClassDto
    {
        public string ClassName { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string AcademicYear { get; set; } = string.Empty;
        public string? ClassTeacherId { get; set; }
    }
}
