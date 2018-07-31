using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Utilities;
using NLog;

namespace EDiary.Services
{
    public class SchoolClassesService : ISchoolClassesService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SchoolClassesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<SchoolClass> GetSchoolClasses()
        {
            return db.SchoolClassRepository.Get();
        }

        public SchoolClass GetSchoolClassById(string id)
        {
            SchoolClass schoolClass = db.SchoolClassRepository.GetByID(id);

            return schoolClass;
        }

        public SchoolClass PostSchoolClass(SchoolClassDTO schoolClassDto)
        {
            if (db.SchoolClassRepository.GetByID(schoolClassDto.Id) != null)
            {
                return null;
            }

            SchoolClass schoolClass = Utils.ConvertSchoolClassDTOToSchoolClass(schoolClassDto, db);          
            if (schoolClass == null)
            {
                return null;
            }
            db.SchoolClassRepository.Insert(schoolClass);
            db.Save();
            logger.Info("Added new school class");

            return schoolClass;
        }

        public bool PutSchoolClass(string id, SchoolClassDTO schoolClassDto)
        {
            if (id != schoolClassDto.Id)
            {
                return false;
            }
           
            SchoolClass checkSchoolClass = db.SchoolClassRepository.GetByID(id);
            if (checkSchoolClass == null)
            {
                return false;
            }
            checkSchoolClass.SchoolYear = schoolClassDto.SchoolYear;
            checkSchoolClass.ClassNumber = schoolClassDto.ClassNumber;
            checkSchoolClass.CalendarYear = schoolClassDto.CalendarYear; 
            Teacher headTeacher = db.TeacherRepository.GetByID(schoolClassDto.HeadTeacherId);
            
            if (headTeacher == null || (headTeacher.HeadClass != null && headTeacher.HeadClass != checkSchoolClass))  // proveravamo da li je headTeacher vec razredni nekom razredu (ne zelimo da dva razreda imaju istog razrednog), i jos proveravamo da li headTeacher sa tim Id-jem uopste postoji.
            {
                return false;
            }
            
            checkSchoolClass.HeadTeacher = headTeacher;
            db.SchoolClassRepository.Update(checkSchoolClass);
            db.Save();
            logger.Info("Updated school class (id:{0})", checkSchoolClass.Id);

            return true;
        }

        public SchoolClass DeleteSchoolClass(string id)
        {
            SchoolClass schoolClass = db.SchoolClassRepository.GetByID(id);
            if (schoolClass == null)
            {
                return null;
            }

            db.SchoolClassRepository.Delete(id);
            db.Save();
            logger.Info("Deleted school class (id:{0})", id);

            return schoolClass;
        }

        public SchoolClass PutHeadTeacher(string id, string teacherId)
        {
            SchoolClass schoolClass = db.SchoolClassRepository.GetByID(id);
            if (schoolClass == null)
            {
                return null;
            }

            Teacher headTeacher = db.TeacherRepository.GetByID(teacherId);
            if (headTeacher == null || headTeacher.HeadClass != null)  // proveravamo da li je headTeacher vec razredni nekom razredu (ne zelimo da dva razreda imaju istog razrednog), i jos proveravamo da li headTeacher sa tim Id-jem uopste postoji.
            {
                return null;
            }          

            schoolClass.HeadTeacher = headTeacher;
            db.SchoolClassRepository.Update(schoolClass);
            db.Save();

            return schoolClass;
        }

        public SchoolClass PutStudent(string id, string studentId)
        {
            SchoolClass schoolClass = db.SchoolClassRepository.GetByID(id);

            if (schoolClass == null)
            {
                return null;
            }

            Student student = db.StudentRepository.GetByID(studentId);

            if (student == null)
            {
                return null;
            }

            schoolClass.Students.Add(student);
            db.SchoolClassRepository.Update(schoolClass);
            db.Save();

            return schoolClass;
        }       

        public SchoolClass PutTeacherTeachSubjectToSchoolClass(string id, int teacherTeachSubjectToSchoolClassId)
        {
            SchoolClass schoolClass = db.SchoolClassRepository.GetByID(id);

            if (schoolClass == null)
            {
                return null;
            }

            TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass = db.TeacherTeachSubjectToSchoolClassRepository.GetByID(teacherTeachSubjectToSchoolClassId);

            if (teacherTeachSubjectToSchoolClass == null)
            {
                return null;
            }

            schoolClass.TeacherTeachSubjectToSchoolClasses.Add(teacherTeachSubjectToSchoolClass);
            db.SchoolClassRepository.Update(schoolClass);
            db.Save();

            return schoolClass;
        }

        public IEnumerable<SchoolClass> GetSchoolClassesByCalendarYear(int calendarYear)
        {
            IEnumerable<SchoolClass> schoolClasses = db.SchoolClassRepository.Get(x => x.CalendarYear == calendarYear);

            return schoolClasses;
        }

        public IEnumerable<SchoolClass> GetSchoolClassesByCalendarYearAndSchoolYear(int calendarYear, SchoolYearEnum schoolYear)
        {
            IEnumerable<SchoolClass> schoolClasses = db.SchoolClassRepository.Get(x => x.CalendarYear == calendarYear && x.SchoolYear == schoolYear);

            return schoolClasses;
        }

        public IEnumerable<StudentDTO> GetSchoolClassStudents(string id)
        {
            SchoolClass schoolClass = db.SchoolClassRepository.GetByID(id);
            List<StudentDTO> studentsDto = new List<StudentDTO>();
            foreach (var item in schoolClass.Students)
            {
                studentsDto.Add(Utils.ConvertStudentToDto(item));
            }

            return studentsDto;
            //return schoolClass.Students;
        }

        public IEnumerable<TeacherDTO> GetSchoolClassTeachers(string id)
        {
            SchoolClass schollClass = db.SchoolClassRepository.GetByID(id);
            if (schollClass == null)
            {
                return null;
            }
            IEnumerable<Teacher> teachers = schollClass.TeacherTeachSubjectToSchoolClasses
                .Select(x => x.TeacherTeachSubject = db.TeacherTeachSubjectRepository.GetByID(x.TeacherTeachSubjectId))
                .Select(x => x.Teacher = db.TeacherRepository.GetByID(x.TeacherId));
            if (teachers == null)
            {
                return null;
            }
            List<TeacherDTO> teachersDto = new List<TeacherDTO>();
            foreach (var item in teachers)
            {
                teachersDto.Add(Utils.ConvertTeacherToDto(item));
            }

            return teachersDto;
        }

        public IEnumerable<Subject> GetSchoolClassSubjects(string id)
        {
            SchoolClass schollClass = db.SchoolClassRepository.GetByID(id);
            if (schollClass == null)
            {
                return null;
            }
            IEnumerable<Subject> subjects = schollClass.TeacherTeachSubjectToSchoolClasses
                .Select(x => x.TeacherTeachSubject = db.TeacherTeachSubjectRepository.GetByID(x.TeacherTeachSubjectId))
                .Select(x => x.Subject = db.SubjectRepository.GetByID(x.SubjectId));
            if (subjects == null)
            {
                return null;
            }

            return subjects;
        }
    }
}