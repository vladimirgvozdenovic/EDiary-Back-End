using EDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ITeacherTeachSubjectToSchoolClassToStudentsService
    {
        IEnumerable<TeacherTeachSubjectToSchoolClassToStudent> GetTeacherTeachSubjectToSchoolClassToStudents();
        TeacherTeachSubjectToSchoolClassToStudent GetTeacherTeachSubjectToSchoolClassToStudentById(int id);
        TeacherTeachSubjectToSchoolClassToStudent PostTeacherTeachSubjectToSchoolClassToStudent(TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent);
        bool PutTeacherTeachSubjectToSchoolClassToStudent(int id, TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent);
        TeacherTeachSubjectToSchoolClassToStudent DeleteTeacherTeachSubjectToSchoolClassToStudent(int id);
        IEnumerable<TeacherTeachSubjectToSchoolClass> GetTeacherTeachSubjectsToSchoolClassesByStudent(string id);
        IEnumerable<Student> GetStudentsByTeacherTeachSubjectToSchoolClass(int id);
    }
}
