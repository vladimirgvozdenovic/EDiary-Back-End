using EDiary.Models;
using EDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ISubjectsService
    {
        IEnumerable<Subject> GetSubjects();
        Subject GetSubjectById(int id);
        Subject PostSubject(Subject subject);
        bool PutSubject(int id, Subject subject);
        Subject DeleteSubject(int id);
        Subject PutLesson(int id, int lessonId);
        IEnumerable<Subject> GetSubjectsBySchoolYear(SchoolYearEnum schoolYear);
        IEnumerable<Subject> GetSubjectsByName(string pattern);
        IEnumerable<Lesson> GetSubjectLessons(int id);
        IEnumerable<TeacherDTO> GetSubjectTeachers(int id);
    }
}
