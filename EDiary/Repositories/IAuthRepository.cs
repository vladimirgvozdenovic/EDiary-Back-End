using EDiary.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Repositories
{
    public interface IAuthRepository : IDisposable
    {
        Task<IdentityResult> RegisterTeacher(Teacher teacher, string password);
        Task<IdentityResult> RegisterStudent(Student student, string password);
        Task<IdentityResult> RegisterParent(Parent parent, string password);
        Task<IdentityResult> RegisterAdminUser(AdminUser userModel, string password);
        Task<User> FindUser(string userName, string password);
        Task<IList<string>> FindRoles(string userId);
        User FindUserByUserName(string userName);
    }
}
