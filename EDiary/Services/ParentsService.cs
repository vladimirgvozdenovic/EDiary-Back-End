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
    public class ParentsService : IParentsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ParentsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<ParentDTO> GetParents()
        {
            IEnumerable<Parent> parents = db.ParentRepository.Get();
            List<ParentDTO> parentsDto = new List<ParentDTO>();
            ParentDTO parentDto;

            foreach (var item in parents)
            {
                parentDto = Utils.ConvertParentToDto(item);

                parentsDto.Add(parentDto);
            }

            return parentsDto;
        }

        public ParentDTO GetParentById(string id)
        {
            // Parent parent = db.ParentRepository.GetByID(id); // GenericRepository (GetByID(id)) pronalazi i Teacher-e (i sve User-e) ako unesemo njihov ID (zbog nasledjivanja) i onda zapucava, moramo nekako proveravati tip!!

            Parent parent = db.ParentRepository.Get(x => x.Id == id).FirstOrDefault();
            if (parent == null)
            {
                return null;
            }

            ParentDTO parentDto = new ParentDTO();

            parentDto = Utils.ConvertParentToDto(parent);

            return parentDto;
        }

        public async Task<IdentityResult> PostParent(ParentDTO parentDto)
        {
            //pri prvom unosu Parent-a dodeljujemo mu default-ni username i password.
            Parent parent = new Parent();

            parent.UserName = Utils.CreateUserNameForParent(parentDto, db);
            //parent.Password = String.Concat(parentDto.FirstName, "123");
            parent.FirstName = parentDto.FirstName;
            parent.LastName = parentDto.LastName;
            parent.Email = parentDto.Email;
            parent.PhoneNumber = parentDto.PhoneNumber;

            //db.ParentRepository.Insert(parent);
            //db.AuthRepository.RegisterParent(parent, /*parentDto.Password*/String.Concat(parentDto.FirstName, "123"));
            //db.Save();
            logger.Info("Added new parent");
            return await db.AuthRepository.RegisterParent(parent, /*teacherDto.Password*/String.Concat(parentDto.FirstName, "123"));
            //return parent;
        }

        public bool PutParent(string id, ParentDTO parentDto)
        {
            if (id != parentDto.UserId)
            {
                return false;
            }

            Parent checkParent = db.ParentRepository.GetByID(id);
            if (checkParent == null)
            {
                return false;
            }
            checkParent.FirstName = parentDto.FirstName; // Uvodimo DTO zbog username i pass, ne zelim da neko ovim putem ima pristup (mogucnost promene) user-u i pass-u
            checkParent.LastName = parentDto.LastName;
            checkParent.Email = parentDto.Email;
            checkParent.PhoneNumber = parentDto.PhoneNumber;

            db.ParentRepository.Update(checkParent);
            db.Save();
            logger.Info("Updated parent (id:{0})", checkParent.Id);

            return true;
        }

        public Parent DeleteParent(string id)
        {
            Parent parent = db.ParentRepository.GetByID(id);
            if (parent == null || parent.Students.Count != 0)
            {
                return null;
            }

            db.ParentRepository.Delete(id);
            db.Save();
            logger.Info("Deleted parent (id:{0})", id);

            return parent;
        }

        public Parent PutStudent(string id, string studentId)
        {
            Parent parent = db.ParentRepository.GetByID(id);

            if (parent == null)
            {
                return null;
            }

            Student student = db.StudentRepository.GetByID(studentId);

            if (student == null)
            {
                return null;
            }

            parent.Students.Add(student);
            db.ParentRepository.Update(parent);
            db.Save();

            return parent;
        }

        public Parent GetParentByUsername(string username)
        {
            //Parent parent = db.ParentRepository.Get(x => x.UserName == username).FirstOrDefault();
            Parent parent = db.ParentRepository.Get(x => x.UserName == username).FirstOrDefault();

            return parent;
        }

        public IEnumerable<ParentDTO> GetParentsByName(string pattern)
        {
            IEnumerable<Parent> parents = db.ParentRepository.Get(x => x.FirstName == pattern || x.LastName == pattern);
            List<ParentDTO> parentsDto = new List<ParentDTO>();
            ParentDTO parentDto;

            foreach (var item in parents)
            {
                parentDto = Utils.ConvertParentToDto(item);

                parentsDto.Add(parentDto);
            }

            //return students;
            return parentsDto;
        }

        public IEnumerable<Student> GetParentStudents(string id)
        {
            Parent parent = db.ParentRepository.GetByID(id);
            if (parent == null)
            {
                return null;
            }

            return parent.Students;
        }

    }
}