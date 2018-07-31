using EDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ITeacherTeachSubjectToSchoolClassesService
    {
        IEnumerable<TeacherTeachSubjectToSchoolClass> GetTeacherTeachSubjectToSchoolClasses();
        TeacherTeachSubjectToSchoolClass GetTeacherTeachSubjectToSchoolClassById(int id);
        TeacherTeachSubjectToSchoolClass PostTeacherTeachSubjectToSchoolClass(TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass);
        bool PutTeacherTeachSubjectToSchoolClass(int id, TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass);
        TeacherTeachSubjectToSchoolClass DeleteTeacherTeachSubjectToSchoolClass(int id);
        IEnumerable<TeacherTeachSubject> GetTeacherTeachSubjectsBySchoolClass(string id);
        IEnumerable<SchoolClass> GetSchoolClassesByTeacherTeachSubject(int id);
    }
}
