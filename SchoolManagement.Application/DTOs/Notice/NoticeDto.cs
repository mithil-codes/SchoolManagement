using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Notice
{
    public class NoticeDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? PostedByUserId { get; set; }
        public string? PostedByName { get; set; }
        public bool IsActive { get; set; }
    }
}
