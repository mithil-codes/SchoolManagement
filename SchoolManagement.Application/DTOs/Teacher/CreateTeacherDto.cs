using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Teacher
{
    public class CreateTeacherDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
    }
}
