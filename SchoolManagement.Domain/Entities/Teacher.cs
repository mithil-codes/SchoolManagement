using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
        // NAYA — Identity se link
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
