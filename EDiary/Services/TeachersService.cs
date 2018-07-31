using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Utilities;
using Microsoft.AspNet.Identity;
using NLog;

namespace EDiary.Services
{
    public class TeachersService : ITeachersService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TeachersService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<TeacherDTO> GetTeachers()
        {
            IEnumerable<Teacher> teachers = db.TeacherRepository.Get();
            List<TeacherDTO> teachersDto = new List<TeacherDTO>();
            TeacherDTO teacherDto;

            foreach (var item in teachers)
            {
                teacherDto = Utils.ConvertTeacherToDto(item);

                teachersDto.Add(teacherDto);
            }

            //return db.TeacherRepository.Get();
            return teachersDto;
        }

        public TeacherDTO GetTeacherById(string id)
        {
            //Teacher teacher = db.TeacherRepository.GetByID(id);  // GenericRepository (GetByID(id)) pronalazi i Student-e (i sve User-e) ako unesemo njihov ID (zbog nasledjivanja) i onda zapucava, moramo nekako proveravati tip!!

            Teacher teacher = db.TeacherRepository.Get(x => x.Id == id).FirstOrDefault();
            if (teacher == null)
            {
                return null;
            }

            TeacherDTO teacherDto = new TeacherDTO();

            teacherDto = Utils.ConvertTeacherToDto(teacher);

            return teacherDto;
        }

        //public Teacher PostTeacher(TeacherDTO teacherDto) 
        public async Task<IdentityResult> PostTeacher(TeacherDTO teacherDto)
        {
            //pri prvom unosu Teacher-a dodeljujemo mu default-ni username i password i setujemo HeadClass na null vrednost.
            Teacher teacher = new Teacher();           
            
            teacher.UserName = Utils.CreateUserNameForTeacher(teacherDto, db);
            //teacher.Password = String.Concat(teacherDto.FirstName, "123");
            teacher.FirstName = teacherDto.FirstName;
            teacher.LastName = teacherDto.LastName;
            teacher.Email = teacherDto.Email;
            teacher.HeadClass = null;

            //db.TeacherRepository.Insert(teacher);
            //db.AuthRepository.RegisterTeacher(teacher, String.Concat(teacherDto.FirstName, "123"));
            //db.Save();     
            logger.Info("Added new teacher");
            return await db.AuthRepository.RegisterTeacher(teacher, /*teacherDto.Password*/String.Concat(teacherDto.FirstName, "123"));
            //return teacher;
        }

        public bool PutTeacher(string id, TeacherDTO teacherDto)
        {
            if (id != teacherDto.UserId)
            {
                return false;
            }

            Teacher checkTeacher = db.TeacherRepository.GetByID(id);
            if (checkTeacher == null)
            {
                return false;
            }
            checkTeacher.FirstName = teacherDto.FirstName;  // Uvodimo DTO zbog username i pass, ne zelim da neko ovim putem ima pristup user-u i pass-u
            checkTeacher.LastName = teacherDto.LastName;
            checkTeacher.Email = teacherDto.Email;
            db.TeacherRepository.Update(checkTeacher);
            db.Save();
            logger.Info("Updated teacher (id:{0})", checkTeacher.Id);

            return true;
        }

        public Teacher DeleteTeacher(string id)
        {
            Teacher teacher = db.TeacherRepository.GetByID(id);
            if (teacher == null || teacher.HeadClass != null)  // kaskadno brisanje (ne moze se obrisati Teacher koji se nalazi u SchoolClass kao HeadTeacher)
            {
                return null;
            }
            
            db.TeacherRepository.Delete(id);
            db.Save();
            logger.Info("Deleted teacher (id:{0})", id);

            return teacher;
        }

        public Teacher PutHeadClass(string id, string schoolClassId)
        {
            Teacher teacher = db.TeacherRepository.GetByID(id);

            if (teacher == null)
            {
                return null;
            }

            SchoolClass headClass = db.SchoolClassRepository.GetByID(schoolClassId);

            if (headClass == null)
            {
                return null;
            }

            teacher.HeadClass = headClass;
            db.TeacherRepository.Update(teacher);
            db.Save();

            return teacher;
        }

        public Teacher GetTeacherByUsername(string username)
        {
            Teacher teacher = db.TeacherRepository.Get(x => x.UserName == username).FirstOrDefault();

            return teacher;
        }        

        public IEnumerable<TeacherDTO> GetTeachersByName(string pattern)
        {
            IEnumerable<Teacher> teachers = db.TeacherRepository.Get(x => x.FirstName == pattern || x.LastName == pattern);
            List<TeacherDTO> teachersDto = new List<TeacherDTO>();
            TeacherDTO teacherDto;

            foreach (var item in teachers)
            {
                teacherDto = Utils.ConvertTeacherToDto(item);

                teachersDto.Add(teacherDto);
            }

            //return teachers;
            return teachersDto;
        }

        public IEnumerable<Subject> GetTeacherSubjects(string id)
        {
            Teacher teacher = db.TeacherRepository.GetByID(id);
            if (teacher == null)
            {
                return null;
            }

            IEnumerable<Subject> subjects = teacher.TeacherTeachSubjects.Select(x => x.Subject = db.SubjectRepository.GetByID(x.SubjectId));

            return subjects;
        }

        //public bool PutResetPassword(int id)
        //{
        //    Teacher teacher = db.TeacherRepository.GetByID(id);
        //    if (teacher == null)
        //    {
        //        return false;
        //    }
        //    teacher.Password = String.Concat(teacher.FirstName, "123");
        //    //treba implementirati slanje mail-a sa novim passwordom

        //    return true;
        //}

        //public bool PutChangePassword(int id, string oldPass, string newPass)
        //{
        //    Teacher teacher = db.TeacherRepository.GetByID(id);
        //    if (teacher == null)
        //    {
        //        return false;
        //    }
        //    if (oldPass!=teacher.Password)
        //    {
        //        return false;
        //    }
        //    teacher.Password = newPass;

        //    return true;
        //}
    }
}