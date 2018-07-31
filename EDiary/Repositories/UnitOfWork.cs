using EDiary.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity.Attributes;

namespace EDiary.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DbContext context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        //private GenericRepository<User> userRepository; 18 
        [Dependency]
        public IGenericRepository<AdminUser> AdminUserRepository { get; set; }
        [Dependency]
        public IGenericRepository<Student> StudentRepository { get; set; }
        [Dependency]
        public IGenericRepository<Teacher> TeacherRepository { get; set; }
        [Dependency]
        public IGenericRepository<Parent> ParentRepository { get; set; }
        [Dependency]
        public IGenericRepository<Subject> SubjectRepository { get; set; }
        [Dependency]
        public IGenericRepository<Lesson> LessonRepository { get; set; }
        [Dependency]
        public IGenericRepository<Lecture> LectureRepository { get; set; }
        [Dependency]
        public IGenericRepository<SchoolClass> SchoolClassRepository { get; set; }
        [Dependency]
        public IGenericRepository<Semester> SemesterRepository { get; set; }
        //private GenericRepository<Grade> gradeRepository;
        [Dependency]
        public IGenericRepository<SemesterGrade> SemesterGradeRepository { get; set; }
        [Dependency]
        public IGenericRepository<FinaleSemesterGrade> FinaleSemesterGradeRepository { get; set; }
        [Dependency]
        public IGenericRepository<FinaleGrade> FinaleGradeRepository { get; set; }
        [Dependency]
        public IGenericRepository<StudentsAbsence> StudentsAbsenceRepository { get; set; }
        [Dependency]
        public IGenericRepository<TeacherTeachSubject> TeacherTeachSubjectRepository { get; set; }
        [Dependency]
        public IGenericRepository<TeacherTeachSubjectToSchoolClass> TeacherTeachSubjectToSchoolClassRepository { get; set; }
        [Dependency]
        public IGenericRepository<TeacherTeachSubjectToSchoolClassToStudent> TeacherTeachSubjectToSchoolClassToStudentRepository { get; set; }
        [Dependency]
        public IGenericRepository<TeacherTeachSubjectToSchoolClassToStudentAtSemester> TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository { get; set; }
        [Dependency]
        public IAuthRepository AuthRepository { get; set; }

        ////private GenericRepository<User> userRepository; 18
        //private GenericRepository<Student> studentRepository;
        //private GenericRepository<Teacher> teacherRepository;
        //private GenericRepository<Parent> parentRepository;
        //private GenericRepository<Subject> subjectRepository;
        //private GenericRepository<Lesson> lessonRepository;
        //private GenericRepository<Lecture> lectureRepository;
        //private GenericRepository<SchoolClass> schoolClassRepository;
        //private GenericRepository<Semester> semesterRepository;
        ////private GenericRepository<Grade> gradeRepository;
        //private GenericRepository<SemesterGrade> semesterGradeRepository;
        //private GenericRepository<FinaleSemesterGrade> finaleSemesterGradeRepository;
        //private GenericRepository<FinaleGrade> finaleGradeRepository;
        //private GenericRepository<StudentsAbsence> studentsAbsenceRepository;
        //private GenericRepository<TeacherTeachSubject> teacherTeachSubjectRepository;
        //private GenericRepository<TeacherTeachSubjectToSchoolClass> teacherTeachSubjectToSchoolClassRepository;
        //private GenericRepository<TeacherTeachSubjectToSchoolClassToStudent> teacherTeachSubjectToSchoolClassToStudentRepository;
        //private GenericRepository<TeacherTeachSubjectToSchoolClassToStudentAtSemester> teacherTeachSubjectToSchoolClassToStudentAtSemesterRepository;

        /*public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }*/

        //public GenericRepository<Student> StudentRepository
        //{
        //    get
        //    {
        //        if (this.studentRepository == null)
        //        {
        //            this.studentRepository = new GenericRepository<Student>(context);
        //        }
        //        return studentRepository;
        //    }
        //}

        //public GenericRepository<Teacher> TeacherRepository
        //{
        //    get
        //    {
        //        if (this.teacherRepository == null)
        //        {
        //            this.teacherRepository = new GenericRepository<Teacher>(context);
        //        }
        //        return teacherRepository;
        //    }
        //}

        //public GenericRepository<Parent> ParentRepository
        //{
        //    get
        //    {
        //        if (this.parentRepository == null)
        //        {
        //            this.parentRepository = new GenericRepository<Parent>(context);
        //        }
        //        return parentRepository;
        //    }
        //}

        //public GenericRepository<Subject> SubjectRepository
        //{
        //    get
        //    {
        //        if (this.subjectRepository == null)
        //        {
        //            this.subjectRepository = new GenericRepository<Subject>(context);
        //        }
        //        return subjectRepository;
        //    }
        //}

        //public GenericRepository<Lesson> LessonRepository
        //{
        //    get
        //    {
        //        if (this.lessonRepository == null)
        //        {
        //            this.lessonRepository = new GenericRepository<Lesson>(context);
        //        }
        //        return lessonRepository;
        //    }
        //}

        //public GenericRepository<Lecture> LectureRepository
        //{
        //    get
        //    {
        //        if (this.lectureRepository == null)
        //        {
        //            this.lectureRepository = new GenericRepository<Lecture>(context);
        //        }
        //        return lectureRepository;
        //    }
        //}

        //public GenericRepository<SchoolClass> SchoolClassRepository
        //{
        //    get
        //    {
        //        if (this.schoolClassRepository == null)
        //        {
        //            this.schoolClassRepository = new GenericRepository<SchoolClass>(context);
        //        }
        //        return schoolClassRepository;
        //    }
        //}

        //public GenericRepository<Semester> SemesterRepository
        //{
        //    get
        //    {
        //        if (this.semesterRepository == null)
        //        {
        //            this.semesterRepository = new GenericRepository<Semester>(context);
        //        }
        //        return semesterRepository;
        //    }
        //}      

        /*public GenericRepository<Grade> GradeRepository
        {
            get
            {
                if (this.gradeRepository == null)
                {
                    this.gradeRepository = new GenericRepository<Grade>(context);
                }
                return gradeRepository;
            }
        }*/

        //public GenericRepository<SemesterGrade> SemesterGradeRepository
        //{
        //    get
        //    {
        //        if (this.semesterGradeRepository == null)
        //        {
        //            this.semesterGradeRepository = new GenericRepository<SemesterGrade>(context);
        //        }
        //        return semesterGradeRepository;
        //    }
        //}

        //public GenericRepository<FinaleSemesterGrade> FinaleSemesterGradeRepository
        //{
        //    get
        //    {
        //        if (this.finaleSemesterGradeRepository == null)
        //        {
        //            this.finaleSemesterGradeRepository = new GenericRepository<FinaleSemesterGrade>(context);
        //        }
        //        return finaleSemesterGradeRepository;
        //    }
        //}

        //public GenericRepository<FinaleGrade> FinaleGradeRepository
        //{
        //    get
        //    {
        //        if (this.finaleGradeRepository == null)
        //        {
        //            this.finaleGradeRepository = new GenericRepository<FinaleGrade>(context);
        //        }
        //        return finaleGradeRepository;
        //    }
        //}

        //public GenericRepository<StudentsAbsence> StudentsAbsenceRepository
        //{
        //    get
        //    {
        //        if (this.studentsAbsenceRepository == null)
        //        {
        //            this.studentsAbsenceRepository = new GenericRepository<StudentsAbsence>(context);
        //        }
        //        return studentsAbsenceRepository;
        //    }
        //}

        //public GenericRepository<TeacherTeachSubject> TeacherTeachSubjectRepository
        //{
        //    get
        //    {
        //        if (this.teacherTeachSubjectRepository == null)
        //        {
        //            this.teacherTeachSubjectRepository = new GenericRepository<TeacherTeachSubject>(context);
        //        }
        //        return teacherTeachSubjectRepository;
        //    }
        //}

        //public GenericRepository<TeacherTeachSubjectToSchoolClass> TeacherTeachSubjectToSchoolClassRepository
        //{
        //    get
        //    {
        //        if (this.teacherTeachSubjectToSchoolClassRepository == null)
        //        {
        //            this.teacherTeachSubjectToSchoolClassRepository = new GenericRepository<TeacherTeachSubjectToSchoolClass>(context);
        //        }
        //        return teacherTeachSubjectToSchoolClassRepository;
        //    }
        //}

        //public GenericRepository<TeacherTeachSubjectToSchoolClassToStudent> TeacherTeachSubjectToSchoolClassToStudentRepository
        //{
        //    get
        //    {
        //        if (this.teacherTeachSubjectToSchoolClassToStudentRepository == null)
        //        {
        //            this.teacherTeachSubjectToSchoolClassToStudentRepository = new GenericRepository<TeacherTeachSubjectToSchoolClassToStudent>(context);
        //        }
        //        return teacherTeachSubjectToSchoolClassToStudentRepository;
        //    }
        //}

        //public GenericRepository<TeacherTeachSubjectToSchoolClassToStudentAtSemester> TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository
        //{
        //    get
        //    {
        //        if (this.teacherTeachSubjectToSchoolClassToStudentAtSemesterRepository == null)
        //        {
        //            this.teacherTeachSubjectToSchoolClassToStudentAtSemesterRepository = new GenericRepository<TeacherTeachSubjectToSchoolClassToStudentAtSemester>(context);
        //        }
        //        return teacherTeachSubjectToSchoolClassToStudentAtSemesterRepository;
        //    }
        //}

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error("Failed save changes to database"); // Logujemo svaki neuspeli upis u bazu!
                throw ex;
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}