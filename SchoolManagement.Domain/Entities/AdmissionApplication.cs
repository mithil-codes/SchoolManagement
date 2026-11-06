using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Domain.Entities
{
    public class AdmissionApplication
    {
     
            public int Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; }
            public string Gender { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;

            public string ParentName { get; set; } = string.Empty;
            public string ParentPhone { get; set; } = string.Empty;

            public int DesiredClassId { get; set; }
            public Class DesiredClass { get; set; } = null!;

            public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
            public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

            public int? CreatedStudentId { get; set; }   // Approve hone ke baad, Student ki ID yahan save hogi
        }
    }


