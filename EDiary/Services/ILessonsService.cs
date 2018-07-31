using EDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ILessonsService
    {
        IEnumerable<Lesson> GetLessons();
        Lesson GetLessonById(int id);
        Lesson PostLesson(Lesson lesson);
        bool PutLesson(int id, Lesson lesson);
        Lesson DeleteLesson(int id);
        Lesson PutSubject(int id, int subjectId);
        IEnumerable<Lesson> GetLessonsByName(string pattern);
        IEnumerable<Lecture> GetLessonLectures(int id);
    }
}
