using EDiary.Models;
using EDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class TeacherTeachSubjectToSchoolClassesService : ITeacherTeachSubjectToSchoolClassesService
    {
        private IUnitOfWork db;

        public TeacherTeachSubjectToSchoolClassesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<TeacherTeachSubjectToSchoolClass> GetTeacherTeachSubjectToSchoolClasses()
        {
            return db.TeacherTeachSubjectToSchoolClassRepository.Get();
        }

        public TeacherTeachSubjectToSchoolClass GetTeacherTeachSubjectToSchoolClassById(int id)
        {
            TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass = db.TeacherTeachSubjectToSchoolClassRepository.GetByID(id);

            return teacherTeachSubjectToSchoolClass;
        }

        public TeacherTeachSubjectToSchoolClass PostTeacherTeachSubjectToSchoolClass(TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass)
        {
            TeacherTeachSubject teacherTeachSubject = db.TeacherTeachSubjectRepository.GetByID(teacherTeachSubjectToSchoolClass.TeacherTeachSubjectId);
            SchoolClass schoolClass = db.SchoolClassRepository.GetByID(teacherTeachSubjectToSchoolClass.SchoolClassId);
            if (teacherTeachSubject == null || schoolClass == null)
            {
                return null;
            }
            TeacherTeachSubjectToSchoolClass newTeacherTeachSubjectToSchoolClass = new TeacherTeachSubjectToSchoolClass();
            newTeacherTeachSubjectToSchoolClass.TeacherTeachSubject = teacherTeachSubject;
            newTeacherTeachSubjectToSchoolClass.SchoolClass = schoolClass;
            
            db.TeacherTeachSubjectToSchoolClassRepository.Insert(newTeacherTeachSubjectToSchoolClass);

            //-----------------Za svaki dodati "teacherTeachSubjectToSchoolClass" napunimo "teacherTeachSubjectToSchoolClassToStudent"-------------------------
            //-----------------Svaki Student koji ide u taj SchoolClass dobije taj Subject koji predaje taj Teacher tom SchoolClass-u
            TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent;
            TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester;
            foreach (var item in schoolClass.Students)
            {
                teacherTeachSubjectToSchoolClassToStudent = new TeacherTeachSubjectToSchoolClassToStudent();
                teacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClassId = teacherTeachSubjectToSchoolClass.Id;
                teacherTeachSubjectToSchoolClassToStudent.StudentId = item.Id;
                db.TeacherTeachSubjectToSchoolClassToStudentRepository.Insert(teacherTeachSubjectToSchoolClassToStudent);

                // Dodajemo "teacherTeachSubjectToSchoolClassToStudentAtSemester" za oba semestra
                teacherTeachSubjectToSchoolClassToStudentAtSemester = new TeacherTeachSubjectToSchoolClassToStudentAtSemester();
                teacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent = teacherTeachSubjectToSchoolClassToStudent;
                teacherTeachSubjectToSchoolClassToStudentAtSemester.Semester = db.SemesterRepository.GetByID(SemesterEnum.FIRST);
                db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Insert(teacherTeachSubjectToSchoolClassToStudentAtSemester);

                teacherTeachSubjectToSchoolClassToStudentAtSemester = new TeacherTeachSubjectToSchoolClassToStudentAtSemester();
                teacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent = teacherTeachSubjectToSchoolClassToStudent;
                teacherTeachSubjectToSchoolClassToStudentAtSemester.Semester = db.SemesterRepository.GetByID(SemesterEnum.SECOND);
                db.TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository.Insert(teacherTeachSubjectToSchoolClassToStudentAtSemester);
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------

            db.Save();

            return teacherTeachSubjectToSchoolClass;
        }

        public bool PutTeacherTeachSubjectToSchoolClass(int id, TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass)
        {
            if (id != teacherTeachSubjectToSchoolClass.Id)
            {
                return false;
            }
            if (db.TeacherTeachSubjectToSchoolClassToStudentRepository.Get(x => x.TeacherTeachSubjectToSchoolClassId == id) != null) // Ne smemo da menjamo "TeacherTeachSubjectToSchoolClass" ako je vec vezan za "TeacherTeachSubjectToSchoolClassToStudent", jer je TeacherTeachSubject vezan za SchoolClass i dalje za Student-e i ocene
            {
                return false;
            }

            TeacherTeachSubjectToSchoolClass checkTeacherTeachSubjectToSchoolClass = db.TeacherTeachSubjectToSchoolClassRepository.GetByID(id);
            if (checkTeacherTeachSubjectToSchoolClass == null)
            {
                return false;
            }
            checkTeacherTeachSubjectToSchoolClass.TeacherTeachSubject = db.TeacherTeachSubjectRepository.GetByID(teacherTeachSubjectToSchoolClass.TeacherTeachSubjectId);
            checkTeacherTeachSubjectToSchoolClass.SchoolClass = db.SchoolClassRepository.GetByID(teacherTeachSubjectToSchoolClass.SchoolClassId);

            db.TeacherTeachSubjectToSchoolClassRepository.Update(checkTeacherTeachSubjectToSchoolClass);
            db.Save();

            return true;
        }

        public TeacherTeachSubjectToSchoolClass DeleteTeacherTeachSubjectToSchoolClass(int id)
        {
            TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass = db.TeacherTeachSubjectToSchoolClassRepository.GetByID(id);
            if (teacherTeachSubjectToSchoolClass == null)
            {
                return null;
            }

            db.TeacherTeachSubjectToSchoolClassRepository.Delete(id);
            db.Save();

            return teacherTeachSubjectToSchoolClass;
        }

        public IEnumerable<TeacherTeachSubject> GetTeacherTeachSubjectsBySchoolClass(string id)
        {
            IEnumerable<TeacherTeachSubject> teacherTeachSubjects = db.TeacherTeachSubjectToSchoolClassRepository.Get(x => x.SchoolClassId == id).Select(x => x.TeacherTeachSubject = db.TeacherTeachSubjectRepository.GetByID(x.TeacherTeachSubjectId));

            return teacherTeachSubjects;
        }

        public IEnumerable<SchoolClass> GetSchoolClassesByTeacherTeachSubject(int id)
        {
            IEnumerable<SchoolClass> schoolClasses = db.TeacherTeachSubjectToSchoolClassRepository.Get(x => x.TeacherTeachSubjectId == id).Select(x => x.SchoolClass = db.SchoolClassRepository.GetByID(x.SchoolClassId));

            return schoolClasses;
        }
    }
}