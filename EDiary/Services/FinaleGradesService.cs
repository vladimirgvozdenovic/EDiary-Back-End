using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class FinaleGradesService : IFinaleGradesService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public FinaleGradesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<FinaleGrade> GetFinaleGrades()
        {
            return db.FinaleGradeRepository.Get();
        }

        public FinaleGrade GetFinaleGradeById(int id)
        {
            // FinaleGrade finaleGrade = db.FinaleGradeRepository.GetByID(id); // GenericRepository (GetByID(id)) pronalazi i FinaleSemesterGrades (i sve Grade) ako unesemo njihov ID (zbog nasledjivanja) i onda zapucava, moramo nekako proveravati tip!!
            FinaleGrade finaleGrade = db.FinaleGradeRepository.Get(x => x.GradeId == id).FirstOrDefault();
            if (finaleGrade == null)
            {
                return null;
            }

            return finaleGrade;
        }

        public FinaleGrade PostFinaleGrade(FinaleGradeDTO finaleGradeDto)
        {
            TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentRepository.GetByID(finaleGradeDto.TeacherTeachSubjectToSchoolClassToStudentId);
            if (teacherTeachSubjectToSchoolClassToStudent == null)
            {
                return null;
            }

            // Ako vec postoji FinaleGrade za dati TeacherTeachSubjectToSchoolClassToStudent vracamo null
            FinaleGrade check = db.FinaleGradeRepository.Get(x => x.TeacherTeachSubjectToSchoolClassToStudent.Id == teacherTeachSubjectToSchoolClassToStudent.Id).FirstOrDefault();
            if (check != null)
            {
                return null;
            }
            FinaleGrade finaleGrade = new FinaleGrade();
            finaleGrade.Mark = finaleGradeDto.Mark;
            finaleGrade.Date = DateTime.Now;
            finaleGrade.TeacherTeachSubjectToSchoolClassToStudent = teacherTeachSubjectToSchoolClassToStudent;
            db.FinaleGradeRepository.Insert(finaleGrade);
            db.Save();

            logger.Info("Added new finale grade");
            Utilities.Utils.FinaleGradeMailSender(finaleGrade);

            return finaleGrade;    
        }

        public bool PutFinaleGrade(int id, FinaleGradeDTO finaleGradeDto)
        {
            if (id != finaleGradeDto.GradeId)
            {
                return false;
            }

            FinaleGrade checkFinaleGrade = db.FinaleGradeRepository.GetByID(id);
            if (checkFinaleGrade == null)
            {
                return false;
            }
            checkFinaleGrade.Mark = finaleGradeDto.Mark;
            checkFinaleGrade.Date = DateTime.Now;
            checkFinaleGrade.TeacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentRepository.GetByID(finaleGradeDto.TeacherTeachSubjectToSchoolClassToStudentId);

            db.FinaleGradeRepository.Update(checkFinaleGrade);
            db.Save();

            logger.Info("Updated finale grade (id:{0})", checkFinaleGrade.GradeId);
            return true;
        }

        public FinaleGrade DeleteFinaleGrade(int id)
        {
            FinaleGrade finaleGrade = db.FinaleGradeRepository.GetByID(id);
            if (finaleGrade == null)
            {
                return null;
            }

            db.FinaleGradeRepository.Delete(id);
            db.Save();
            logger.Info("Deleted finale grade (id:{0})", id);

            return finaleGrade;
        }

        public IEnumerable<FinaleGrade> GetFinaleGradesByDate(DateTime date)
        {
            IEnumerable<FinaleGrade> finaleGrades = db.FinaleGradeRepository.Get(x => x.Date.Day == date.Date.Day);

            return finaleGrades;
        }
    }
}