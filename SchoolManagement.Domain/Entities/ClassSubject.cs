using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class ClassSubject
    {
        public int Id { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; } = null!;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        // FK → ApplicationUser (Identity)
        public string? TeacherId { get; set; }

        public ApplicationUser? Teacher { get; set; }
    }
}
