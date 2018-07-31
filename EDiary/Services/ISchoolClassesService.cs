using EDiary.Models;
using EDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ISchoolClassesService
    {
        IEnumerable<SchoolClass> GetSchoolClasses();
        SchoolClass GetSchoolClassById(string id);
        SchoolClass PostSchoolClass(SchoolClassDTO schoolClassDto);
        bool PutSchoolClass(string id, SchoolClassDTO schoolClassDto);
        SchoolClass DeleteSchoolClass(string id);
        SchoolClass PutHeadTeacher(string id, string teacherId);
        SchoolClass PutTeacherTeachSubjectToSchoolClass(string id, int teacherTeachSubjectToSchoolClassId);
        SchoolClass PutStudent(string id, string studentId);
        IEnumerable<SchoolClass> GetSchoolClassesByCalendarYear(int calendarYear);
        IEnumerable<SchoolClass> GetSchoolClassesByCalendarYearAndSchoolYear(int calendarYear, SchoolYearEnum schoolYear);
        IEnumerable<StudentDTO> GetSchoolClassStudents(string id);
        IEnumerable<TeacherDTO> GetSchoolClassTeachers(string id);
        IEnumerable<Subject> GetSchoolClassSubjects(string id);
    }
}
