using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EDiary.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork db;

        public UserService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public async Task<IdentityResult> RegisterAdmin(AdminUser userModel)
        {
            AdminUser admin = new AdminUser
            {
                UserName = Utils.CreateUserNameForAdmin(userModel, db),
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber
            };

            return await db.AuthRepository.RegisterAdminUser(admin, /*userModel.Password*/String.Concat(userModel.FirstName, "123"));
        }

        public async Task<IdentityResult> RegisterTeacher(TeacherDTO teacherDto)
        {
            Teacher teacher = new Teacher
            {
                UserName = Utils.CreateUserNameForTeacher(teacherDto, db),
                //UserName = userModel.UserName,
                FirstName = teacherDto.FirstName,
                LastName = teacherDto.LastName,
                Email = teacherDto.Email,
                HeadClass = null
            };

            return await db.AuthRepository.RegisterTeacher(teacher, /*teacherDto.Password*/String.Concat(teacherDto.FirstName, "123"));
        }

        public async Task<IdentityResult> RegisterStudent(StudentDTO studentDto)
        {
            Student student = new Student
            {
                UserName = Utils.CreateUserNameForStudent(studentDto, db),
                //UserName = studentDto.UserName,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Parent = null,
                SchoolClass = db.SchoolClassRepository.GetByID(studentDto.SchoolClassId)
        };

            return await db.AuthRepository.RegisterStudent(student, /*studentDto.Password*/String.Concat(studentDto.FirstName, "123"));
        }

        public async Task<IdentityResult> RegisterParent(ParentDTO parentDto)
        {
            Parent parent = new Parent
            {
                UserName = Utils.CreateUserNameForParent(parentDto, db),
                //UserName = userModel.UserName,
                FirstName = parentDto.FirstName,
                LastName = parentDto.LastName,
                Email = parentDto.Email,
                PhoneNumber = parentDto.PhoneNumber
            };

            return await db.AuthRepository.RegisterParent(parent, /*parentDto.Password*/String.Concat(parentDto.FirstName, "123"));
        }
    }
}