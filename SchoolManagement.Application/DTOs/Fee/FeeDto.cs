using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Fee
{
    public class FeeDto
    {

        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string FeeType { get; set; } = string.Empty;
        public double TotalAmount { get; set; }
        public double PaidAmount { get; set; }
        public double DueAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AcademicYear { get; set; } = string.Empty;
    }
}

