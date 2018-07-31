using EDiary.Models;
using EDiary.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class SemestersService : ISemestersService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SemestersService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Semester> GetSemesters()
        {
            return db.SemesterRepository.Get();
        }

        public Semester GetSemesterById(SemesterEnum id)
        {
            Semester semester = db.SemesterRepository.GetByID(id);

            return semester;
        }

        public Semester PostSemester(Semester semester)
        {
            if (semester.Name != SemesterEnum.FIRST && semester.Name != SemesterEnum.SECOND)
            {
                return null;
            }
            db.SemesterRepository.Insert(semester);       
            db.Save();
            logger.Info("Added new semester");

            return semester;
        }

        public bool PutSemester(SemesterEnum id, Semester semester)
        {
            if (id != semester.Name)
            {
                return false;
            }

            Semester checkSemester = db.SemesterRepository.GetByID(id);
            if (checkSemester == null)
            {
                return false;
            }
            checkSemester.StartDate = semester.StartDate;
            checkSemester.EndDate = semester.EndDate;

            db.SemesterRepository.Update(checkSemester);
            db.Save();
            logger.Info("Updated semester (id:{0})", checkSemester.Name);

            return true;
        }

        public Semester DeleteSemester(SemesterEnum id)
        {
            Semester semester = db.SemesterRepository.GetByID(id);
            if (semester == null)
            {
                return null;
            }

            db.SemesterRepository.Delete(id);
            db.Save();
            logger.Info("Deleted semester (id:{0})", id);

            return semester;
        }
    }
}