using EDiary.Models;
using EDiary.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class StudentsAbsencesService : IStudentsAbsencesService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public StudentsAbsencesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<StudentsAbsence> GetStudentsAbsences()
        {
            return db.StudentsAbsenceRepository.Get();
        }

        public StudentsAbsence GetStudentsAbsenceById(int id)
        {
            StudentsAbsence studentsAbsence = db.StudentsAbsenceRepository.GetByID(id);

            return studentsAbsence;
        }

        public StudentsAbsence PostStudentsAbsence(StudentsAbsence studentsAbsence)
        {
            Student student = db.StudentRepository.GetByID(studentsAbsence.StudentId);
            Lecture lecture = db.LectureRepository.GetByID(studentsAbsence.LectureId);
            if (student == null || lecture == null)
            {
                return null;
            }
            StudentsAbsence newStudentsAbsence = new StudentsAbsence();
            newStudentsAbsence.Justified = false;
            newStudentsAbsence.Student = student;
            newStudentsAbsence.Lecture = lecture;

            db.StudentsAbsenceRepository.Insert(newStudentsAbsence);
            db.Save();
            logger.Info("Added new students absence");

            return newStudentsAbsence;
        }

        public bool PutStudentsAbsence(int id, StudentsAbsence studentsAbsence)
        {
            if (id != studentsAbsence.Id)
            {
                return false;
            }

            StudentsAbsence checkStudentsAbsence = db.StudentsAbsenceRepository.GetByID(id);
            if (checkStudentsAbsence == null)
            {
                return false;
            }
            checkStudentsAbsence.Justified = studentsAbsence.Justified;

            db.StudentsAbsenceRepository.Update(checkStudentsAbsence);
            db.Save();
            logger.Info("Updated students absence (id:{0})", checkStudentsAbsence.Id);

            return true;
        }

        public StudentsAbsence DeleteStudentsAbsence(int id)
        {
            StudentsAbsence studentsAbsence = db.StudentsAbsenceRepository.GetByID(id);
            if (studentsAbsence == null)
            {
                return null;
            }

            db.StudentsAbsenceRepository.Delete(id);
            db.Save();
            logger.Info("Deleted students absence (id:{0})", id);

            return studentsAbsence;
        }

        public IEnumerable<StudentsAbsence> GetStudentsAbsencesByStudent(string id)
        {
            Student student = db.StudentRepository.GetByID(id);

            return student.StudentsAbsences;
        }

        public IEnumerable<StudentsAbsence> GetStudentsAbsencesByLecture(int id)
        {
            Lecture lecture = db.LectureRepository.GetByID(id);

            return lecture.Absence;
        }

        public IEnumerable<StudentsAbsence> GetStudentsAbsencesByDate(DateTime date)
        {
            IEnumerable<StudentsAbsence> studentsAbsences = db.StudentsAbsenceRepository.Get(x => x.Lecture.Date.Day == date.Date.Day);

            return studentsAbsences;
        }
       
    }
}