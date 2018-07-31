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
    public interface ITeachersService
    {
        IEnumerable<TeacherDTO> GetTeachers();
        TeacherDTO GetTeacherById(string id);
        //Teacher PostTeacher(TeacherDTO teacherDto);
        Task<IdentityResult> PostTeacher(TeacherDTO teacherDto);
        bool PutTeacher(string id, TeacherDTO teacherDto);
        Teacher DeleteTeacher(string id);
        Teacher PutHeadClass(string id, string schoolClassId);
        Teacher GetTeacherByUsername(string username);
        IEnumerable<TeacherDTO> GetTeachersByName(string pattern);
        IEnumerable<Subject> GetTeacherSubjects(string id);
        //bool PutResetPassword(int id);
        //bool PutChangePassword(int id, string oldPass, string newPass);
    }
}
