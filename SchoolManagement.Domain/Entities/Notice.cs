using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    public class Notice
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }

        public string? PostedByUserId { get; set; }
        public ApplicationUser? PostedByUser { get; set; }     // ye add karo

        public bool IsActive { get; set; } = true;
    }
}
