using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDiary.Services
{
    public class TeacherTeachSubjectsService : ITeacherTeachSubjectsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TeacherTeachSubjectsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<TeacherTeachSubject> GetTeacherTeachSubjects()
        {
            return db.TeacherTeachSubjectRepository.Get();
        }

        public TeacherTeachSubject GetTeacherTeachSubjectById(int id)
        {
            TeacherTeachSubject teacherTeachSubject = db.TeacherTeachSubjectRepository.GetByID(id);

            return teacherTeachSubject;
        }

        public TeacherTeachSubject PostTeacherTeachSubject(TeacherTeachSubject teacherTeachSubject)
        {
            Teacher teacher = db.TeacherRepository.GetByID(teacherTeachSubject.TeacherId);
            Subject subject = db.SubjectRepository.GetByID(teacherTeachSubject.SubjectId);
            if (teacher == null || subject == null)
            {
                return null;
            }
            TeacherTeachSubject newTeacherTeachSubject = new TeacherTeachSubject();
            newTeacherTeachSubject.Teacher = teacher;
            newTeacherTeachSubject.Subject = subject;
            db.TeacherTeachSubjectRepository.Insert(newTeacherTeachSubject);
            db.Save();
            logger.Info("Added new teacher-teach-subject");

            return teacherTeachSubject;
        }

        public bool PutTeacherTeachSubject(int id, TeacherTeachSubject teacherTeachSubject)
        {
            if (id != teacherTeachSubject.Id)
            {
                return false;
            }
            if (db.TeacherTeachSubjectToSchoolClassRepository.Get(x => x.TeacherTeachSubjectId == id) != null) // Ne smemo da menjamo "TeacherTeachSubject" ako je vec vezan za "TeacherTeachSubjectToSchoolClass", jer je Subject vezan za SchoolClass i dalje za Student-e i ocene
            {
                return false;  
            }

            TeacherTeachSubject checkTeacherTeachSubject = db.TeacherTeachSubjectRepository.GetByID(id);
            if (checkTeacherTeachSubject == null)
            {
                return false;
            }
            checkTeacherTeachSubject.Teacher = db.TeacherRepository.GetByID(teacherTeachSubject.TeacherId);
            checkTeacherTeachSubject.Subject = db.SubjectRepository.GetByID(teacherTeachSubject.SubjectId); 

            db.TeacherTeachSubjectRepository.Update(checkTeacherTeachSubject);
            db.Save();
            logger.Info("Updated teacher-teach-subject (id:{0})", checkTeacherTeachSubject.Id);

            return true;
        }

        public TeacherTeachSubject DeleteTeacherTeachSubject(int id)
        {
            TeacherTeachSubject teacherTeachSubject = db.TeacherTeachSubjectRepository.GetByID(id);
            if (teacherTeachSubject == null)
            {
                return null;
            }

            db.TeacherTeachSubjectRepository.Delete(id);
            db.Save();
            logger.Info("Deleted teacher-teach-subject (id:{0})", id);

            return teacherTeachSubject;
        }

        public IEnumerable<Subject> GetSubjectsByTeacher(string id)
        {
            IEnumerable<Subject> subjects = db.TeacherTeachSubjectRepository.Get( x => x.TeacherId == id).Select(x => x.Subject = db.SubjectRepository.GetByID(x.SubjectId));

            return subjects;
        }

        public IEnumerable<TeacherDTO> GetTeachersBySubject(int id)
        {
            IEnumerable <Teacher> teachers = db.TeacherTeachSubjectRepository.Get(x => x.SubjectId == id).Select(x => x.Teacher = db.TeacherRepository.GetByID(x.TeacherId));
            List<TeacherDTO> teachersDto = new List<TeacherDTO>();
            foreach (var item in teachers)
            {
                teachersDto.Add(Utils.ConvertTeacherToDto(item));
            }

            return teachersDto;
        }
    }
}