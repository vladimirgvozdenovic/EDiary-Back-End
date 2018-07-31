using EDiary.Models;
using EDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class TeacherTeachSubjectToSchoolClassToStudentAtSemestersService : ITeacherTeachSubjectToSchoolClassToStudentAtSemestersService
    {
        private IUnitOfWork db;

        public TeacherTeachSubjectToSchoolClassToStudentAtSemestersService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<TeacherTeachSubjectToSchoolClassToStudentAtSemester> GetTeacherTeachSubjectToSchoolClassToStudentAtSemesters()
        {
            return db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Get();
        }

        public TeacherTeachSubjectToSchoolClassToStudentAtSemester GetTeacherTeachSubjectToSchoolClassToStudentAtSemesterById(int id)
        {
            TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(id);

            return teacherTeachSubjectToSchoolClassToStudentAtSemester;
        }

        public TeacherTeachSubjectToSchoolClassToStudentAtSemester PostTeacherTeachSubjectToSchoolClassToStudentAtSemester(TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester)
        {
            TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentRepository.GetByID(teacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudentId);
            if (teacherTeachSubjectToSchoolClassToStudent == null)
            {
                return null;
            }
            TeacherTeachSubjectToSchoolClassToStudentAtSemester newTeacherTeachSubjectToSchoolClassToStudentAtSemester = new TeacherTeachSubjectToSchoolClassToStudentAtSemester();            
            newTeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent = teacherTeachSubjectToSchoolClassToStudent;
            newTeacherTeachSubjectToSchoolClassToStudentAtSemester.FinaleSemesterGrade = null;
            //newTeacherTeachSubjectToSchoolClassToStudentAtSemester.SemesterName = teacherTeachSubjectToSchoolClassToStudentAtSemester.SemesterName;
            newTeacherTeachSubjectToSchoolClassToStudentAtSemester.Semester = db.SemesterRepository.GetByID(teacherTeachSubjectToSchoolClassToStudentAtSemester.SemesterName);

            db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Insert(newTeacherTeachSubjectToSchoolClassToStudentAtSemester);
            db.Save();

            return newTeacherTeachSubjectToSchoolClassToStudentAtSemester;

            //// ------------------------------------ samo za punjenje baze ----------------------------------------------------
            //TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemesterFill;
            //foreach (var item in db.TeacherTeachSubjectToSchoolClassToStudentRepository.Get())
            //{
            //    // Dodajemo "teacherTeachSubjectToSchoolClassToStudentAtSemester" za oba semestra
            //    teacherTeachSubjectToSchoolClassToStudentAtSemesterFill = new TeacherTeachSubjectToSchoolClassToStudentAtSemester();
            //    teacherTeachSubjectToSchoolClassToStudentAtSemesterFill.TeacherTeachSubjectToSchoolClassToStudent = item;
            //    teacherTeachSubjectToSchoolClassToStudentAtSemesterFill.Semester = db.SemesterRepository.GetByID(SemesterEnum.FIRST);
            //    db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Insert(teacherTeachSubjectToSchoolClassToStudentAtSemesterFill);

            //    teacherTeachSubjectToSchoolClassToStudentAtSemesterFill = new TeacherTeachSubjectToSchoolClassToStudentAtSemester();
            //    teacherTeachSubjectToSchoolClassToStudentAtSemesterFill.TeacherTeachSubjectToSchoolClassToStudent = item;
            //    teacherTeachSubjectToSchoolClassToStudentAtSemesterFill.Semester = db.SemesterRepository.GetByID(SemesterEnum.SECOND);
            //    db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Insert(teacherTeachSubjectToSchoolClassToStudentAtSemesterFill);
            //}
            //db.Save();
            //return null;
            //// ----------------------------------------------------------------------------------------------------------------          
        }

        public bool PutTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id, TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester)
        {
            if (id != teacherTeachSubjectToSchoolClassToStudentAtSemester.Id)
            {
                return false;
            }

            TeacherTeachSubjectToSchoolClassToStudentAtSemester checkTeacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(id);
            if (checkTeacherTeachSubjectToSchoolClassToStudentAtSemester == null)
            {
                return false;
            }
            checkTeacherTeachSubjectToSchoolClassToStudentAtSemester.Semester = db.SemesterRepository.GetByID(teacherTeachSubjectToSchoolClassToStudentAtSemester.SemesterName);
            checkTeacherTeachSubjectToSchoolClassToStudentAtSemester.FinaleSemesterGrade = teacherTeachSubjectToSchoolClassToStudentAtSemester.FinaleSemesterGrade;
            checkTeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentRepository.GetByID(teacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudentId);

            db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Update(checkTeacherTeachSubjectToSchoolClassToStudentAtSemester);
            db.Save();

            return true;
        }

        public TeacherTeachSubjectToSchoolClassToStudentAtSemester DeleteTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id)
        {
            TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(id);
            if (teacherTeachSubjectToSchoolClassToStudentAtSemester == null)
            {
                return null;
            }

            db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Delete(id);
            db.Save();

            return teacherTeachSubjectToSchoolClassToStudentAtSemester;
        }

        public IEnumerable<TeacherTeachSubjectToSchoolClassToStudentAtSemester> GetTeacherTeachSubjectToSchoolClassToStudentAtSemesterByTeacherTeachSubjectToSchoolClassToStudent(int teacherTeachSubjectToSchoolClassToStudentId)
        {
            IEnumerable<TeacherTeachSubjectToSchoolClassToStudentAtSemester> teacherTeachSubjectToSchoolClassToStudentAtSemester = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Get(x => x.TeacherTeachSubjectToSchoolClassToStudentId == teacherTeachSubjectToSchoolClassToStudentId);

            return teacherTeachSubjectToSchoolClassToStudentAtSemester;
        }

        public IEnumerable<TeacherTeachSubjectToSchoolClassToStudent> GetTeacherTeachSubjectToSchoolClassToStudentsBySemester(SemesterEnum id)
        {
            IEnumerable<TeacherTeachSubjectToSchoolClassToStudent> teacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Get(x => x.SemesterName == id).Select(x => x.TeacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentRepository.GetByID(x.TeacherTeachSubjectToSchoolClassToStudentId));

            return teacherTeachSubjectToSchoolClassToStudent;
        }

        public IEnumerable<SemesterGrade> GetSemesterGrades(int id)
        {
            IEnumerable<SemesterGrade> semesterGrades = db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.GetByID(id).SemesterGrades;
            return semesterGrades;
        }
    }
}