using EDiary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EDiary.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private DbContext _ctx;

        private UserManager<User> _userManager;

        public AuthRepository(DbContext context)
        {
            _ctx = context;
            _userManager = new UserManager<User>(new UserStore<User>(_ctx));
        }


        public async Task<IdentityResult> RegisterTeacher(Teacher teacher, string password)
        {
            var result = await _userManager.CreateAsync(teacher, password);
            _userManager.AddToRole(teacher.Id, "teachers");
            return result;
        }
        public async Task<IdentityResult> RegisterStudent(Student student, string password)
        {
            var result = await _userManager.CreateAsync(student, password);
            _userManager.AddToRole(student.Id, "students");
            return result;
        }
        public async Task<IdentityResult> RegisterParent(Parent parent, string password)
        {
            var result = await _userManager.CreateAsync(parent, password);
            _userManager.AddToRole(parent.Id, "parents");
            return result;
        }
        public async Task<IdentityResult> RegisterAdminUser(AdminUser userModel, string password)
        {
            var result = await _userManager.CreateAsync(userModel, password);
            _userManager.AddToRole(userModel.Id, "admins");
            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public User FindUserByUserName(string userName)
        {
            User user = _userManager.FindByName(userName);
            return user;
        }

        public async Task<IList<string>> FindRoles(string userId)
        {
            return await _userManager.GetRolesAsync(userId);
        }

        //In this case you will be treating ChangePassword as Reset Password. You can achieve this by using reset password by generating token and using that token straightaway to validate it with new password.

        //var userId = User.Identity.GetUserId();

        //var token = await UserManager.GeneratePasswordResetTokenAsync(userId);

        //var result = await UserManager.ResetPasswordAsync(userId, token, newPassword);

    public void Dispose()
        {
            //_ctx.Dispose();
            _userManager.Dispose();
        }
    }
}