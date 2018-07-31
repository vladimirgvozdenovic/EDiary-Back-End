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
    public class FinaleSemesterGradesService : IFinaleSemesterGradesService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public FinaleSemesterGradesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<FinaleSemesterGrade> GetFinaleSemesterGrades()
        {
            return db.FinaleSemesterGradeRepository.Get();
        }

        public FinaleSemesterGrade GetFinaleSemesterGradeById(int id)
        {
            // FinaleSemesterGrade finaleSemesterGrade = db.FinaleSemesterGradeRepository.GetByID(id); // GenericRepository (GetByID(id)) pronalazi i FinaleGrades (i sve Grade) ako unesemo njihov ID (zbog nasledjivanja) i onda zapucava, moramo nekako proveravati tip!!
            FinaleSemesterGrade finaleSemesterGrade = db.FinaleSemesterGradeRepository.Get(x => x.GradeId == id).FirstOrDefault();
            if (finaleSemesterGrade == null)
            {
                return null;
            }

            return finaleSemesterGrade;
        }

        public FinaleSemesterGrade PostFinaleSemesterGrade(FinaleSemesterGradeDTO finaleSemesterGradeDto)
        {
            TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(finaleSemesterGradeDto.TeacherTeachSubjectToSchoolClassToStudentAtSemesterId);
            if (teacherTeachSubjectToSchoolClassToStudentAtSemester == null)
            {
                return null;
            }

            // Ako vec postoji FinaleSemesterGrade za dati TeacherTeachSubjectToSchoolClassToStudentAtSemester vracamo null
            FinaleSemesterGrade check = db.FinaleSemesterGradeRepository.Get(x => x.TeacherTeachSubjectToSchoolClassToStudentAtSemester.Id == teacherTeachSubjectToSchoolClassToStudentAtSemester.Id).FirstOrDefault();
            if (check != null)
            {
                return null;
            }
            FinaleSemesterGrade finaleSemesterGrade = new FinaleSemesterGrade();
            finaleSemesterGrade.Mark = finaleSemesterGradeDto.Mark;
            finaleSemesterGrade.Date = DateTime.Now;
            finaleSemesterGrade.TeacherTeachSubjectToSchoolClassToStudentAtSemester = teacherTeachSubjectToSchoolClassToStudentAtSemester;
            db.FinaleSemesterGradeRepository.Insert(finaleSemesterGrade);
            db.Save();

            logger.Info("Added new finale semester grade");
            Utilities.Utils.FinaleSemesterGradeMailSender(finaleSemesterGrade);

            return finaleSemesterGrade;
        }

        public bool PutFinaleSemesterGrade(int id, FinaleSemesterGradeDTO finaleSemesterGradeDto)
        {
            if (id != finaleSemesterGradeDto.GradeId)
            {
                return false;
            }

            FinaleSemesterGrade checkFinaleSemesterGrade = db.FinaleSemesterGradeRepository.GetByID(id);
            if (checkFinaleSemesterGrade == null)
            {
                return false;
            }
            checkFinaleSemesterGrade.Mark = finaleSemesterGradeDto.Mark;
            checkFinaleSemesterGrade.Date = DateTime.Now;
            checkFinaleSemesterGrade.TeacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(finaleSemesterGradeDto.TeacherTeachSubjectToSchoolClassToStudentAtSemesterId);

            db.FinaleSemesterGradeRepository.Update(checkFinaleSemesterGrade);
            db.Save();
            logger.Info("Updated finale semester grade (id:{0})", checkFinaleSemesterGrade.GradeId);

            return true;
        }

        public FinaleSemesterGrade DeleteFinaleSemesterGrade(int id)
        {
            FinaleSemesterGrade finaleSemesterGrade = db.FinaleSemesterGradeRepository.GetByID(id);
            if (finaleSemesterGrade == null)
            {
                return null;
            }

            db.FinaleSemesterGradeRepository.Delete(id);
            db.Save();
            logger.Info("Deleted finale semester grade (id:{0})", id);

            return finaleSemesterGrade;
        }

        public IEnumerable<FinaleSemesterGrade> GetFinaleSemesterGradesByDate(DateTime date)
        {
            IEnumerable<FinaleSemesterGrade> finaleSemesterGrades = db.FinaleSemesterGradeRepository.Get(x => x.Date.Day == date.Date.Day);

            return finaleSemesterGrades;
        }
    }
}