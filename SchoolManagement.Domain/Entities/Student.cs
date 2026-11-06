using SchoolManagement.Domain.Enums;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AdmissionNumber { get; set; } = string.Empty;

        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;

        // NAYA — Identity se link
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
