using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Utilities;
using Microsoft.AspNet.Identity;
using NLog;

namespace EDiary.Services
{
    public class AdminUsersService : IAdminUsersService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AdminUsersService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserDTO> GetAdminUsers()
        {
            IEnumerable<AdminUser> admins = db.AdminUserRepository.Get();
            List<AdminUserDTO> adminsDto = new List<AdminUserDTO>();
            AdminUserDTO adminDto;

            foreach (var item in admins)
            {
                adminDto = Utils.ConvertAdminUserToDto(item);

                adminsDto.Add(adminDto);
            }

            return adminsDto;
        }

        public AdminUserDTO GetAdminUserById(string id)
        {
            AdminUser admin = db.AdminUserRepository.Get(x => x.Id == id).FirstOrDefault();
            if (admin == null)
            {
                return null;
            }

            AdminUserDTO adminDto = new AdminUserDTO();

            adminDto = Utils.ConvertAdminUserToDto(admin);

            return adminDto;
        }

        public async Task<IdentityResult> PostAdminUser(AdminUserDTO adminDto)
        {
            AdminUser admin = new AdminUser();

            admin.UserName = Utils.CreateUserNameForAdminUser(adminDto, db);
            admin.FirstName = adminDto.FirstName;
            admin.LastName = adminDto.LastName;
            admin.Email = adminDto.Email;
            admin.PhoneNumber = adminDto.PhoneNumber;

            logger.Info("Added new admin");
            return await db.AuthRepository.RegisterAdminUser(admin, String.Concat(adminDto.FirstName, "123"));
        }

        public bool PutAdminUser(string id, AdminUserDTO adminDto)
        {
            if (id != adminDto.UserId)
            {
                return false;
            }

            AdminUser checkAdminUser = db.AdminUserRepository.GetByID(id);
            if (checkAdminUser == null)
            {
                return false;
            }
            checkAdminUser.FirstName = adminDto.FirstName; // Uvodimo DTO zbog username i pass, ne zelim da neko ovim putem ima pristup (mogucnost promene) user-u i pass-u
            checkAdminUser.LastName = adminDto.LastName;
            checkAdminUser.Email = adminDto.Email;
            checkAdminUser.PhoneNumber = adminDto.PhoneNumber;
            db.AdminUserRepository.Update(checkAdminUser);
            db.Save();
            logger.Info("Updated admin (id:{0})", checkAdminUser.Id);

            return true;
        }

        public AdminUser DeleteAdminUser(string id)
        {
            AdminUser admin = db.AdminUserRepository.GetByID(id);
            if (admin == null)
            {
                return null;
            }

            db.AdminUserRepository.Delete(id);
            db.Save();
            logger.Info("Deleted admin (id:{0})", id);

            return admin;
        }

        public AdminUser GetAdminUserByUsername(string username)
        {
            AdminUser admin = db.AdminUserRepository.Get(x => x.UserName == username).FirstOrDefault();

            return admin;
        }
    }
}