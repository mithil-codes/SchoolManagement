using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Fee
{
    public class CreateFeeDto
    {
        public int StudentId { get; set; }
        public string FeeType { get; set; } = string.Empty;
        public double TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        public string AcademicYear { get; set; } = string.Empty;
    }
}
