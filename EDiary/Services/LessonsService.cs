using EDiary.Models;
using EDiary.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class LessonsService : ILessonsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LessonsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Lesson> GetLessons()
        {
            return db.LessonRepository.Get();
        }

        public Lesson GetLessonById(int id)
        {
            Lesson lesson = db.LessonRepository.GetByID(id);

            return lesson;
        }

        public Lesson PostLesson(Lesson lesson)
        {
            Subject subject = db.SubjectRepository.GetByID(lesson.SubjectId);
            if (subject == null)
            {
                return null;
            }
            Lesson newLesson = new Lesson();
            newLesson.Name = lesson.Name;
            newLesson.Subject = subject;
            db.LessonRepository.Insert(newLesson);
            db.Save();
            logger.Info("Added new lesson");

            return lesson;
        }

        public bool PutLesson(int id, Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return false;
            }

            Lesson checkLesson = db.LessonRepository.GetByID(id);
            if (checkLesson == null)
            {
                return false;
            }
            checkLesson.Name = lesson.Name;
            checkLesson.Subject = db.SubjectRepository.GetByID(lesson.SubjectId);

            db.LessonRepository.Update(checkLesson);
            db.Save();
            logger.Info("Updated lesson (id:{0})", checkLesson.Id);

            return true;
        }

        public Lesson DeleteLesson(int id)
        {
            Lesson lesson = db.LessonRepository.GetByID(id);
            if (lesson == null)
            {
                return null;
            }

            db.LessonRepository.Delete(id);
            db.Save();
            logger.Info("Deleted lesson (id:{0})", id);

            return lesson;
        }

        public Lesson PutSubject(int id, int subjectId)
        {
            Lesson lesson = db.LessonRepository.GetByID(id);
            if (lesson == null)
            {
                return null;
            }

            Subject subject = db.SubjectRepository.GetByID(subjectId);
            if (subject == null)
            {
                return null;
            }

            lesson.Subject = subject;
            db.LessonRepository.Update(lesson);
            db.Save();

            return lesson;
        }

        public IEnumerable<Lesson> GetLessonsByName(string pattern)
        {
            IEnumerable<Lesson> lessons = db.LessonRepository.Get(x => x.Name == pattern);

            return lessons;
        }

        public IEnumerable<Lecture> GetLessonLectures(int id)
        {
            Lesson lesson = db.LessonRepository.GetByID(id);

            return lesson.Lectures;
        }
    }
}