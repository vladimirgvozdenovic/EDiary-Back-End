using EDiary.Models;
using EDiary.Models.DTOs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface IAdminUsersService
    {
        IEnumerable<AdminUserDTO> GetAdminUsers();
        AdminUserDTO GetAdminUserById(string id);
        Task<IdentityResult> PostAdminUser(AdminUserDTO adminDto);
        bool PutAdminUser(string id, AdminUserDTO adminDto);
        AdminUser DeleteAdminUser(string id);
        AdminUser GetAdminUserByUsername(string username);
    }
}
