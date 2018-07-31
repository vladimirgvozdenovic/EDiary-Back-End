using EDiary.Models;
using EDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class TeacherTeachSubjectToSchoolClassToStudentsService : ITeacherTeachSubjectToSchoolClassToStudentsService
    {
        private IUnitOfWork db;

        public TeacherTeachSubjectToSchoolClassToStudentsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<TeacherTeachSubjectToSchoolClassToStudent> GetTeacherTeachSubjectToSchoolClassToStudents()
        {
            return db.TeacherTeachSubjectToSchoolClassToStudentRepository.Get();
        }

        public TeacherTeachSubjectToSchoolClassToStudent GetTeacherTeachSubjectToSchoolClassToStudentById(int id)
        {
            TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentRepository.GetByID(id);

            return teacherTeachSubjectToSchoolClassToStudent;
        }

        public TeacherTeachSubjectToSchoolClassToStudent PostTeacherTeachSubjectToSchoolClassToStudent(TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent)
        {
            TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass = db.TeacherTeachSubjectToSchoolClassRepository.GetByID(teacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClassId);
            Student student = db.StudentRepository.GetByID(teacherTeachSubjectToSchoolClassToStudent.StudentId);
            if (teacherTeachSubjectToSchoolClass == null || student == null)
            {
                return null;
            }
            TeacherTeachSubjectToSchoolClassToStudent newTeacherTeachSubjectToSchoolClassToStudent = new TeacherTeachSubjectToSchoolClassToStudent();
            newTeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass = teacherTeachSubjectToSchoolClass;
            newTeacherTeachSubjectToSchoolClassToStudent.Student = student;
            newTeacherTeachSubjectToSchoolClassToStudent.FinaleGrade = null;

            db.TeacherTeachSubjectToSchoolClassToStudentRepository.Insert(newTeacherTeachSubjectToSchoolClassToStudent);
            db.Save();

            return newTeacherTeachSubjectToSchoolClassToStudent;

            //// ------------------------------------ samo za punjenje baze ----------------------------------------------------
            //foreach (var studentItem in db.StudentRepository.Get())
            //{
            //    TeacherTeachSubjectToSchoolClassToStudent teacherTSubjectToSchoolClassToStudent;
            //    Student student = db.StudentRepository.GetByID(studentItem.Id);
            //    SchoolClass schoolClass = db.SchoolClassRepository.GetByID(student.SchoolClassId);
            //    foreach (var item in schoolClass.TeacherTeachSubjectToSchoolClasses)
            //    {
            //        teacherTSubjectToSchoolClassToStudent = new TeacherTeachSubjectToSchoolClassToStudent();
            //        teacherTSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass = item;
            //        teacherTSubjectToSchoolClassToStudent.Student = studentItem;
            //        db.TeacherTeachSubjectToSchoolClassToStudentRepository.Insert(teacherTSubjectToSchoolClassToStudent);
            //    }
            //}
            //db.Save();
            //return null;
            //// ----------------------------------------------------------------------------------------------------------------          
        }

        public bool PutTeacherTeachSubjectToSchoolClassToStudent(int id, TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent)
        {
            if (id != teacherTeachSubjectToSchoolClassToStudent.Id)
            {
                return false;
            }

            TeacherTeachSubjectToSchoolClassToStudent checkTeacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentRepository.GetByID(id);
            if (checkTeacherTeachSubjectToSchoolClassToStudent == null)
            {
                return false;
            }
            
            db.TeacherTeachSubjectToSchoolClassToStudentRepository.Update(checkTeacherTeachSubjectToSchoolClassToStudent);
            db.Save();

            return true;
        }

        public TeacherTeachSubjectToSchoolClassToStudent DeleteTeacherTeachSubjectToSchoolClassToStudent(int id)
        {
            TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent = db.TeacherTeachSubjectToSchoolClassToStudentRepository.GetByID(id);
            if (teacherTeachSubjectToSchoolClassToStudent == null)
            {
                return null;
            }

            db.TeacherTeachSubjectToSchoolClassToStudentRepository.Delete(id);
            db.Save();

            return teacherTeachSubjectToSchoolClassToStudent;
        }

        public IEnumerable<TeacherTeachSubjectToSchoolClass> GetTeacherTeachSubjectsToSchoolClassesByStudent(string id)
        {
            IEnumerable<TeacherTeachSubjectToSchoolClass> teacherTeachSubjectToSchoolClasses = db.TeacherTeachSubjectToSchoolClassToStudentRepository.Get(x => x.StudentId == id).Select(x => x.TeacherTeachSubjectToSchoolClass = db.TeacherTeachSubjectToSchoolClassRepository.GetByID(x.TeacherTeachSubjectToSchoolClassId));

            return teacherTeachSubjectToSchoolClasses;
        }

        public IEnumerable<Student> GetStudentsByTeacherTeachSubjectToSchoolClass(int id)
        {
            IEnumerable<Student> students = db.TeacherTeachSubjectToSchoolClassToStudentRepository.Get(x => x.TeacherTeachSubjectToSchoolClassId == id).Select(x => x.Student = db.StudentRepository.GetByID(x.StudentId));

            return students;
        }

    }
}