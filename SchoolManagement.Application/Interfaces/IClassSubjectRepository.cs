using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IClassSubjectRepository
    {
        Task<ClassSubject?> GetByIdAsync(int id);
        Task<(List<ClassSubject> ClassSubjects, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<ClassSubject> AddAsync(ClassSubject classSubject);
        Task UpdateAsync(ClassSubject classSubject);
        Task DeleteAsync(ClassSubject classSubject);

        Task<List<ClassSubject>> GetByTeacherIdAsync(string teacherId);


    }
}
