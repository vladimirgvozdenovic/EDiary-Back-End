using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class SubjectsService : ISubjectsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SubjectsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Subject> GetSubjects()
        {
            return db.SubjectRepository.Get();
        }

        public Subject GetSubjectById(int id)
        {
            Subject subject = db.SubjectRepository.GetByID(id);

            return subject;
        }

        public Subject PostSubject(Subject subject)
        {
            db.SubjectRepository.Insert(subject);
            db.Save();
            logger.Info("Added new subject");

            return subject;
        }

        public bool PutSubject(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return false;
            }

            Subject checkSubject = db.SubjectRepository.GetByID(id);
            if (checkSubject == null)
            {
                return false;
            }
            checkSubject.Name = subject.Name;
            checkSubject.SchoolYear = subject.SchoolYear;
            checkSubject.WeeklyLectureQuantity = subject.WeeklyLectureQuantity;

            db.SubjectRepository.Update(checkSubject);
            db.Save();
            logger.Info("Updated subject (id:{0})", checkSubject.Id);

            return true;
        }

        public Subject DeleteSubject(int id)
        {
            Subject subject = db.SubjectRepository.GetByID(id);
            if (subject == null)
            {
                return null;
            }

            db.SubjectRepository.Delete(id);
            db.Save();
            logger.Info("Deleted subject (id:{0})", id);

            return subject;
        }

        public Subject PutLesson(int id, int lessonId)
        {
            Subject subject = db.SubjectRepository.GetByID(id);
            if (subject == null)
            {
                return null;
            }

            Lesson lesson = db.LessonRepository.GetByID(lessonId);
            if (lesson == null)
            {
                return null;
            }

            subject.Lessons.Add(lesson);
            db.SubjectRepository.Update(subject);
            db.Save();

            return subject;
        }

        public IEnumerable<Subject> GetSubjectsBySchoolYear(SchoolYearEnum schoolYear)
        {
            IEnumerable<Subject> subjects = db.SubjectRepository.Get(x => x.SchoolYear == schoolYear);

            return subjects;
        }

        public IEnumerable<Subject> GetSubjectsByName(string pattern)
        {
            IEnumerable<Subject> subjects = db.SubjectRepository.Get(x => x.Name == pattern);

            return subjects;
        }

        public IEnumerable<Lesson> GetSubjectLessons(int id)
        {
            Subject subject = db.SubjectRepository.GetByID(id);

            return subject.Lessons;
        }

        public IEnumerable<TeacherDTO> GetSubjectTeachers(int id)
        {
            Subject subject = db.SubjectRepository.GetByID(id);
            if (subject == null)
            {
                return null;
            }

            IEnumerable<Teacher> teachers = subject.TeacherTeachSubjects.Select(x => x.Teacher = db.TeacherRepository.GetByID(x.TeacherId));
            List<TeacherDTO> teachersDto = new List<TeacherDTO>();
            foreach (var item in teachers)
            {
                teachersDto.Add(Utils.ConvertTeacherToDto(item));
            }

            return teachersDto;
        }
    }
}