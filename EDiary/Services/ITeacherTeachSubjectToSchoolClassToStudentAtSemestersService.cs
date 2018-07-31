using EDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ITeacherTeachSubjectToSchoolClassToStudentAtSemestersService
    {
        IEnumerable<TeacherTeachSubjectToSchoolClassToStudentAtSemester> GetTeacherTeachSubjectToSchoolClassToStudentAtSemesters();
        TeacherTeachSubjectToSchoolClassToStudentAtSemester GetTeacherTeachSubjectToSchoolClassToStudentAtSemesterById(int id);
        TeacherTeachSubjectToSchoolClassToStudentAtSemester PostTeacherTeachSubjectToSchoolClassToStudentAtSemester(TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester);
        bool PutTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id, TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester);
        TeacherTeachSubjectToSchoolClassToStudentAtSemester DeleteTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id);
        IEnumerable<TeacherTeachSubjectToSchoolClassToStudentAtSemester> GetTeacherTeachSubjectToSchoolClassToStudentAtSemesterByTeacherTeachSubjectToSchoolClassToStudent(int teacherTeachSubjectToSchoolClassToStudentId);
        IEnumerable<TeacherTeachSubjectToSchoolClassToStudent> GetTeacherTeachSubjectToSchoolClassToStudentsBySemester(SemesterEnum id);
        IEnumerable<SemesterGrade> GetSemesterGrades(int id);
    }
}
