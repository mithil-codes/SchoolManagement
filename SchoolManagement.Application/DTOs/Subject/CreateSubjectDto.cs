using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Subject
{
    public class CreateSubjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
