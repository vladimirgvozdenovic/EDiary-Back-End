using EDiary.Models;
using EDiary.Models.DTOs;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EDiary.Services
{
    public interface IStudentsService
    {
        IEnumerable<StudentDTO> GetStudents();
        StudentDTO GetStudentById(string id);
        Task<IdentityResult> PostStudent(StudentDTO studentDto);
        bool PutStudent(string id, StudentDTO studentDto);
        Student DeleteStudent(string id);
        Student PutSchoolClass(string id, string schoolClassId);
        Student PutParent(string id, string parentId);
        Student GetStudentByUsername(string username);
        IEnumerable<StudentDTO> GetStudentsByName(string pattern);
    }
}