using SchoolManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Fee
    {
            public int Id { get; set; }

            public int StudentId { get; set; }
            public Student Student { get; set; } = null!;

            public string FeeType { get; set; } = string.Empty;   // "Tuition Fee", "Exam Fee", "Transport Fee"
            public double TotalAmount { get; set; }
            public double PaidAmount { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime? PaidDate { get; set; }
            public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
            public string AcademicYear { get; set; } = string.Empty;
        
    }
}
