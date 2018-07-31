using EDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Services
{
    public interface ILecturesService
    {
        IEnumerable<Lecture> GetLectures();
        Lecture GetLectureById(int id);
        Lecture PostLecture(Lecture lecture);
        bool PutLecture(int id, Lecture lecture);
        Lecture DeleteLecture(int id);
        IEnumerable<Lecture> GetLecturesByDate(DateTime date);
        IEnumerable<Lecture> GetLecturesByTeacherTeachSubjectToSchoolClass(int id);
    }
}
