using SchoolManagement.Domain.Enums;

namespace SchoolManagement.Application.DTOs.Admission
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ParentName { get; set; } = string.Empty;
        public string ParentPhone { get; set; } = string.Empty;
        public string DesiredClassName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime AppliedDate { get; set; }
        public int? CreatedStudentId { get; set; }
    }
}