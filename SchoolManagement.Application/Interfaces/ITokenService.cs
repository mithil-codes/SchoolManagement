using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
