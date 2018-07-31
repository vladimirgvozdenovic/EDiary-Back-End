using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Utilities;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EDiary.Services
{
    public class StudentsService : IStudentsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public StudentsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<StudentDTO> GetStudents()
        {
            IEnumerable<Student> students = db.StudentRepository.Get();
            List<StudentDTO> studentsDto = new List<StudentDTO>();
            StudentDTO studentDto;

            foreach (var item in students)
            {
                studentDto = Utils.ConvertStudentToDto(item);

                studentsDto.Add(studentDto);
            }

            //return db.StudentRepository.Get();
            return studentsDto;           
        }

        public StudentDTO GetStudentById(string id)
        {
            // Student student = db.StudentRepository.GetByID(id); // GenericRepository (GetByID(id)) pronalazi i Teacher-e (i sve User-e) ako unesemo njihov ID (zbog nasledjivanja) i onda zapucava, moramo nekako proveravati tip!!

            Student student = db.StudentRepository.Get(x => x.Id == id).FirstOrDefault();
            if (student == null)
            {
                return null;
            }

            StudentDTO studentDto = new StudentDTO();

            studentDto = Utils.ConvertStudentToDto(student);

            return studentDto;
        }

        //public Student PostStudent(StudentDTO studentDto)
        public async Task<IdentityResult> PostStudent(StudentDTO studentDto)
        {
            //pri prvom unosu Teacher-a dodeljujemo mu default-ni username i password i setujemo Parent na null vrednost.
            Student student = new Student();  // Cim se kreira Student (bilo koja klasa koja nasledjuje User-a) sa "new" automatski mu se dodeljuje vrednost Guid!

            student.UserName = Utils.CreateUserNameForStudent(studentDto, db);
            //student.Password = String.Concat(studentDto.FirstName, "123");
            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.Email = studentDto.Email;
            student.SchoolClass = db.SchoolClassRepository.GetByID(studentDto.SchoolClassId);
            student.Parent = null;

            //db.StudentRepository.Insert(student);
            //db.AuthRepository.RegisterStudent(student, /*parentDto.Password*/String.Concat(studentDto.FirstName, "123"));
            //db.Save();
            logger.Info("Added new student");
            var result = await db.AuthRepository.RegisterStudent(student, String.Concat(studentDto.FirstName, "123"));

            if (result.Succeeded) // dodajemo Student-a u "teacherTeachSubjectToSchoolClassToStudent"
            {
                TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent;
                TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester;
                SchoolClass schoolClass = db.SchoolClassRepository.GetByID(student.SchoolClassId);
                foreach (var item in schoolClass.TeacherTeachSubjectToSchoolClasses) // Student koji je dodat u taj SchoolClass dobija sve Subject-e koje predaju odredjeni Teacher-i tom SchoolClass-u 
                {
                    teacherTeachSubjectToSchoolClassToStudent = new TeacherTeachSubjectToSchoolClassToStudent();
                    teacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClassId = item.Id;
                    teacherTeachSubjectToSchoolClassToStudent.StudentId = student.Id;
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
                db.Save();
            }

            return result;
            //return student;
        }

        public bool PutStudent(string id, StudentDTO studentDto)
        {
            if (id != studentDto.UserId)
            {
                return false;
            }

            Student checkStudent = db.StudentRepository.GetByID(id);
            if (checkStudent == null)
            {
                return false;
            }
            checkStudent.FirstName = studentDto.FirstName; // Uvodimo DTO zbog username i pass, ne zelim da neko ovim putem ima pristup (mogucnost promene) user-u i pass-u
            checkStudent.LastName = studentDto.LastName;
            checkStudent.Email = studentDto.Email;
            checkStudent.Parent = db.ParentRepository.GetByID(studentDto.ParentId);
            checkStudent.SchoolClass = db.SchoolClassRepository.GetByID(studentDto.SchoolClassId);
            db.StudentRepository.Update(checkStudent);
            db.Save();
            logger.Info("Updated student (id:{0})", checkStudent.Id);

            return true;
        }

        public Student DeleteStudent(string id)
        {
            Student student = db.StudentRepository.GetByID(id);
            if (student == null)
            {
                return null;
            }

            db.StudentRepository.Delete(id);
            db.Save();
            logger.Info("Deleted student (id:{0})", id);

            return student;
        }

        public Student PutSchoolClass(string id, string schoolClassId)
        {
            Student student = db.StudentRepository.GetByID(id);

            if (student == null)
            {
                return null;
            }

            SchoolClass schoolClass = db.SchoolClassRepository.GetByID(schoolClassId);

            if (schoolClass == null)
            {
                return null;
            }

            student.SchoolClass = schoolClass;
            db.StudentRepository.Update(student);
            db.Save();

            return student;
        }

        public Student PutParent(string id, string parentId)
        {
            Student student = db.StudentRepository.GetByID(id);

            if (student == null)
            {
                return null;
            }

            Parent parent = db.ParentRepository.GetByID(parentId);

            if (parent == null)
            {
                return null;
            }

            student.Parent = parent;
            db.StudentRepository.Update(student);
            db.Save();

            return student;
        }

        public Student GetStudentByUsername(string username)
        {
            Student student = db.StudentRepository.Get(x => x.UserName == username).FirstOrDefault();

            return student;
        }

        public IEnumerable<StudentDTO> GetStudentsByName(string pattern)
        {
            IEnumerable<Student> students = db.StudentRepository.Get(x => x.FirstName.Contains(pattern) || x.LastName.Contains(pattern));
            List<StudentDTO> studentsDto = new List<StudentDTO>();
            StudentDTO studentDto;

            foreach (var item in students)
            {
                studentDto = Utils.ConvertStudentToDto(item);

                studentsDto.Add(studentDto);
            }

            //return students;
            return studentsDto;
        }

    }
}