using EDiary.Models;
using EDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ITeacherTeachSubjectsService
    {
        IEnumerable<TeacherTeachSubject> GetTeacherTeachSubjects();
        TeacherTeachSubject GetTeacherTeachSubjectById(int id);
        TeacherTeachSubject PostTeacherTeachSubject(TeacherTeachSubject teacherTeachSubject);
        bool PutTeacherTeachSubject(int id, TeacherTeachSubject teacherTeachSubject);
        TeacherTeachSubject DeleteTeacherTeachSubject(int id);
        IEnumerable<Subject> GetSubjectsByTeacher(string id);
        IEnumerable<TeacherDTO> GetTeachersBySubject(int id);
    }
}
