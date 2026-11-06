using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Notice
{
    public class UpdateNoticeDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
