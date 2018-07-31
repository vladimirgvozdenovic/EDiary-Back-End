using EDiary.Models;
using EDiary.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class LecturesService : ILecturesService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LecturesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Lecture> GetLectures()
        {
            return db.LectureRepository.Get();
        }

        public Lecture GetLectureById(int id)
        {
            Lecture lecture = db.LectureRepository.GetByID(id);

            return lecture;
        }

        public Lecture PostLecture(Lecture lecture)
        {
            Lesson lesson = db.LessonRepository.GetByID(lecture.LessonId);
            TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass = db.TeacherTeachSubjectToSchoolClassRepository.GetByID(lecture.TeacherTeachSubjectToSchoolClassId);
            if (lesson == null || teacherTeachSubjectToSchoolClass == null)
            {
                return null;
            }
            Lecture newLecture = new Lecture();
            newLecture.Topic = lecture.Topic;
            newLecture.Date = DateTime.Now;
            newLecture.Lesson = lesson;
            newLecture.TeacherTeachSubjectToSchoolClass = teacherTeachSubjectToSchoolClass;
            db.LectureRepository.Insert(newLecture);
            db.Save();
            logger.Info("Added new lecture");

            return newLecture;
        }

        public bool PutLecture(int id, Lecture lecture)
        {
            if (id != lecture.Id)
            {
                return false;
            }

            Lecture checkLecture = db.LectureRepository.GetByID(id);
            if (checkLecture == null)
            {
                return false;
            }
            checkLecture.Topic = lecture.Topic;

            db.LectureRepository.Update(checkLecture);
            db.Save();
            logger.Info("Updated lecture (id:{0})", checkLecture.Id);

            return true;
        }

        public Lecture DeleteLecture(int id)
        {
            Lecture lecture = db.LectureRepository.GetByID(id);
            if (lecture == null)
            {
                return null;
            }

            db.LectureRepository.Delete(id);
            db.Save();
            logger.Info("Deleted lecture (id:{0})", id);

            return lecture;
        }

        public IEnumerable<Lecture> GetLecturesByDate(DateTime date)
        {
            IEnumerable<Lecture> lectures = db.LectureRepository.Get(x => x.Date.Day == date.Date.Day);

            return lectures;
        }

        public IEnumerable<Lecture> GetLecturesByTeacherTeachSubjectToSchoolClass(int id)
        {
            TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass = db.TeacherTeachSubjectToSchoolClassRepository.GetByID(id);

            return teacherTeachSubjectToSchoolClass.Lectures;
        }
    }
}