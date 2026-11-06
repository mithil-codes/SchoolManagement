using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface IAdmissionRepository
    {
        Task<AdmissionApplication?> GetByIdAsync(int id);
        Task<(List<AdmissionApplication> Applications, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<AdmissionApplication> AddAsync(AdmissionApplication application);
        Task UpdateAsync(AdmissionApplication application);
    }
}
