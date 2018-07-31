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
    public class SemesterGradesService : ISemesterGradesService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SemesterGradesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<SemesterGrade> GetSemesterGrades()
        {
            return db.SemesterGradeRepository.Get();
        }

        public SemesterGrade GetSemesterGradeById(int id)
        {
            // SemesterGrade semesterGrade = db.SemesterGradeRepository.GetByID(id); // GenericRepository (GetByID(id)) pronalazi i FinaleGrades (i sve Grade) ako unesemo njihov ID (zbog nasledjivanja) i onda zapucava, moramo nekako proveravati tip!!
            SemesterGrade semesterGrade = db.SemesterGradeRepository.Get(x => x.GradeId == id).FirstOrDefault();
            if (semesterGrade == null)
            {
                return null;
            }

            return semesterGrade;
        }

        public SemesterGrade PostSemesterGrade(SemesterGradeDTO semesterGradeDto)
        {
            TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(semesterGradeDto.TeacherTeachSubjectToSchoolClassToStudentAtSemesterId);
            if (teacherTeachSubjectToSchoolClassToStudentAtSemester == null)
            {
                return null;
            }
            SemesterGrade semesterGrade = new SemesterGrade();
            semesterGrade.Mark = semesterGradeDto.Mark;
            semesterGrade.Date = DateTime.Now;
            semesterGrade.TeacherTeachSubjectToSchoolClassToStudentAtSemester = teacherTeachSubjectToSchoolClassToStudentAtSemester;
            db.SemesterGradeRepository.Insert(semesterGrade);
            db.Save();
            logger.Info("Added new semester grade");

            Utilities.Utils.GradeMailSender(semesterGrade);

            return semesterGrade;
        }

        public bool PutSemesterGrade(int id, SemesterGradeDTO semesterGradeDto)
        {
            if (id != semesterGradeDto.GradeId)
            {
                return false;
            }

            SemesterGrade checkSemesterGrade = db.SemesterGradeRepository.GetByID(id);
            if (checkSemesterGrade == null)
            {
                return false;
            }
            checkSemesterGrade.Mark = semesterGradeDto.Mark;
            checkSemesterGrade.Date = DateTime.Now;
            checkSemesterGrade.TeacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(semesterGradeDto.TeacherTeachSubjectToSchoolClassToStudentAtSemesterId);

            db.SemesterGradeRepository.Update(checkSemesterGrade);
            db.Save();
            logger.Info("Updated semester grade (id:{0})", checkSemesterGrade.GradeId);

            return true;
        }

        public SemesterGrade DeleteSemesterGrade(int id)
        {
            SemesterGrade semesterGrade = db.SemesterGradeRepository.GetByID(id);
            if (semesterGrade == null)
            {
                return null;
            }

            db.SemesterGradeRepository.Delete(id);
            db.Save();
            logger.Info("Deleted semester grade (id:{0})", id);

            return semesterGrade;
        }

        public IEnumerable<SemesterGrade> GetSemesterGradesByDate(DateTime date)
        {
            IEnumerable<SemesterGrade> semesterGrades = db.SemesterGradeRepository.Get(x => x.Date.Day == date.Date.Day);

            return semesterGrades;
        }

        public IEnumerable<SemesterGrade> GetSemesterGradesByTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id)
        {
            //IEnumerable<SemesterGrade> semesterGrades = db.SemesterGradeRepository.Get(x => x.TeacherTeachSubjectToSchoolClassToStudentAtSemester.Id == id);
            TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(id);

            return teacherTeachSubjectToSchoolClassToStudentAtSemester.SemesterGrades;
        }
    }
}