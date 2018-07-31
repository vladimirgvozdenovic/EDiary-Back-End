using EDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDiary.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<AdminUser> AdminUserRepository { get; }
        IGenericRepository<Student> StudentRepository { get; }
        IGenericRepository<Teacher> TeacherRepository { get; }
        IGenericRepository<Parent> ParentRepository { get; }
        IGenericRepository<Subject> SubjectRepository { get; }
        IGenericRepository<Lesson> LessonRepository { get; }
        IGenericRepository<Lecture> LectureRepository { get; }
        IGenericRepository<SchoolClass> SchoolClassRepository { get; }
        IGenericRepository<Semester> SemesterRepository { get; }
        IGenericRepository<SemesterGrade> SemesterGradeRepository { get; }
        IGenericRepository<FinaleSemesterGrade> FinaleSemesterGradeRepository { get; }
        IGenericRepository<FinaleGrade> FinaleGradeRepository { get; }
        IGenericRepository<StudentsAbsence> StudentsAbsenceRepository { get; }
        IGenericRepository<TeacherTeachSubject> TeacherTeachSubjectRepository { get; }
        IGenericRepository<TeacherTeachSubjectToSchoolClass> TeacherTeachSubjectToSchoolClassRepository { get; }
        IGenericRepository<TeacherTeachSubjectToSchoolClassToStudent> TeacherTeachSubjectToSchoolClassToStudentRepository { get; }
        IGenericRepository<TeacherTeachSubjectToSchoolClassToStudentAtSemester> TeacherTeachSubjectToSchoolClassToStudentAtSemesterRepository { get; }
        IAuthRepository AuthRepository { get; }

        void Save();
    }
}
