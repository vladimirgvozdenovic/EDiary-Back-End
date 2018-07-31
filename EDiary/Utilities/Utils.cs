using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EDiary.Utilities
{
    public static class Utils
    {
        public static TeacherDTO ConvertTeacherToDto(Teacher teacher)
        {
            TeacherDTO teacherDto = new TeacherDTO();

            teacherDto.UserId = teacher.Id;
            teacherDto.FirstName = teacher.FirstName;
            teacherDto.LastName = teacher.LastName;
            teacherDto.Email = teacher.Email;
            teacherDto.TeacherTeachSubjects = teacher.TeacherTeachSubjects;
            if (teacher.HeadClass != null)
            {
                teacherDto.HeadClassId = teacher.HeadClass.Id;
            }
            else
            {
                teacherDto.HeadClassId = null;
            }

            return teacherDto;
        }

        public static string CreateUserNameForTeacher(TeacherDTO teacherDto, IUnitOfWork db)
        {
            int i = 1;
            bool ifExists;
            string username = String.Concat(teacherDto.FirstName.ToLower(), ".", teacherDto.LastName.ToLower());
            do   // do-while koristimo zbog provere da li username vec postoji, ako postoji dodajemo 1 na kraj, ako opet postoji dodajemo 1+1 na kraj i tako dok ne dodjemo do jedinstvenog
            {
                ifExists = false;
                if (db.TeacherRepository.Get(x => x.UserName == username).FirstOrDefault() != null) // radimo proveru da li username vec postoji!!!
                //if (db.AuthRepository.FindUserByUserName(username) != null) // radimo proveru da li username vec postoji!!!
                {
                    username = String.Concat(teacherDto.FirstName.ToLower(), ".", teacherDto.LastName.ToLower(), i++);
                    ifExists = true;
                }
            }
            while (ifExists == true);

            return username;
        }

        public static SchoolClass ConvertSchoolClassDTOToSchoolClass(SchoolClassDTO schoolClassDto, IUnitOfWork db)
        {
            //DbContext context = new DataAccessContext();
            //UnitOfWork db = new UnitOfWork(context);
            SchoolClass schoolClass = new SchoolClass();

            Teacher headTeacher;
            schoolClass.Id = schoolClassDto.Id;
            schoolClass.SchoolYear = schoolClassDto.SchoolYear;
            schoolClass.ClassNumber = schoolClassDto.ClassNumber;
            schoolClass.CalendarYear = schoolClassDto.CalendarYear;
            headTeacher = db.TeacherRepository.GetByID(schoolClassDto.HeadTeacherId);
            if (headTeacher == null || headTeacher.HeadClass != null)  // proveravamo da li je headTeacher vec razredni nekom razredu (ne zelimo da dva razreda imaju istog razrednog), i jos proveravamo da li headTeacher sa tim Id-jem uopste postoji.
            {
                return null;
            }
            schoolClass.HeadTeacher = headTeacher;

            return schoolClass;
        }

        public static StudentDTO ConvertStudentToDto(Student student)
        {
            StudentDTO studentDto = new StudentDTO();

            studentDto.UserId = student.Id;
            studentDto.FirstName = student.FirstName;
            studentDto.LastName = student.LastName;
            studentDto.Email = student.Email;
            studentDto.SchoolClass = student.SchoolClass;
            studentDto.TeacherTeachSubjectToSchoolClassToStudents = student.TeacherTeachSubjectToSchoolClassToStudents;
            studentDto.StudentsAbsences = student.StudentsAbsences;
            if (student.Parent != null)
            {
                studentDto.ParentId = student.ParentId;
            }
            else
            {
                studentDto.ParentId = null;
            }
            studentDto.SchoolClassId = student.SchoolClassId;

            return studentDto;
        }

        public static string CreateUserNameForStudent(StudentDTO studentDto, IUnitOfWork db)
        {
            int i = 1;
            bool ifExists;
            string username = String.Concat(studentDto.FirstName.ToLower(), ".", studentDto.LastName.ToLower());
            do   // do-while koristimo zbog provere da li username vec postoji, ako postoji dodajemo 1 na kraj, ako opet postoji dodajemo 1+1 na kraj i tako dok ne dodjemo do jedinstvenog
            {
                ifExists = false;
                if (db.StudentRepository.Get(x => x.UserName == username).FirstOrDefault() != null) // radimo proveru da li username vec postoji!!!
                //if (db.AuthRepository.FindUserByUserName(username) != null) // radimo proveru da li username vec postoji!!!
                {
                    username = String.Concat(studentDto.FirstName.ToLower(), ".", studentDto.LastName.ToLower(), i++);
                    ifExists = true;
                }
            }
            while (ifExists == true);

            return username;
        }

        public static ParentDTO ConvertParentToDto(Parent parent)
        {
            ParentDTO parentDto = new ParentDTO();

            parentDto.UserId = parent.Id;
            parentDto.FirstName = parent.FirstName;
            parentDto.LastName = parent.LastName;
            parentDto.Email = parent.Email;
            parentDto.PhoneNumber = parent.PhoneNumber;
            parentDto.Students = parent.Students;

            return parentDto;
        }

        public static string CreateUserNameForParent(ParentDTO parentDto, IUnitOfWork db)
        {
            int i = 1;
            bool ifExists;
            string username = String.Concat(parentDto.FirstName.ToLower(), ".", parentDto.LastName.ToLower());
            do   // do-while koristimo zbog provere da li username vec postoji, ako postoji dodajemo 1 na kraj, ako opet postoji dodajemo 1+1 na kraj i tako dok ne dodjemo do jedinstvenog
            {
                ifExists = false;
                if (db.ParentRepository.Get(x => x.UserName == username).FirstOrDefault() != null) // radimo proveru da li username vec postoji!!!
                //if (db.AuthRepository.FindUserByUserName(username) != null) // radimo proveru da li username vec postoji!!!
                {
                    username = String.Concat(parentDto.FirstName.ToLower(), ".", parentDto.LastName.ToLower(), i++);
                    ifExists = true;
                }
            }
            while (ifExists == true);

            return username;
        }

        public static string CreateUserNameForAdmin(AdminUser adminUser, IUnitOfWork db)
        {
            int i = 1;
            bool ifExists;
            string username = String.Concat(adminUser.FirstName.ToLower(), ".", adminUser.LastName.ToLower());
            do   // do-while koristimo zbog provere da li username vec postoji, ako postoji dodajemo 1 na kraj, ako opet postoji dodajemo 1+1 na kraj i tako dok ne dodjemo do jedinstvenog
            {
                ifExists = false;
                if (db.ParentRepository.Get(x => x.UserName == username).FirstOrDefault() != null) // radimo proveru da li username vec postoji!!!
                //if (db.AuthRepository.FindUserByUserName(username) != null) // radimo proveru da li username vec postoji!!!
                {
                    username = String.Concat(adminUser.FirstName.ToLower(), ".", adminUser.LastName.ToLower(), i++);
                    ifExists = true;
                }
            }
            while (ifExists == true);

            return username;
        }

        public static AdminUserDTO ConvertAdminUserToDto(AdminUser admin)
        {
            AdminUserDTO adminDto = new AdminUserDTO();

            adminDto.UserId = admin.Id;
            adminDto.FirstName = admin.FirstName;
            adminDto.LastName = admin.LastName;
            adminDto.Email = admin.Email;
            adminDto.PhoneNumber = admin.PhoneNumber;

            return adminDto;
        }

        public static string CreateUserNameForAdminUser(AdminUserDTO adminDto, IUnitOfWork db)
        {
            int i = 1;
            bool ifExists;
            string username = String.Concat(adminDto.FirstName.ToLower(), ".", adminDto.LastName.ToLower());
            do   // do-while koristimo zbog provere da li username vec postoji, ako postoji dodajemo 1 na kraj, ako opet postoji dodajemo 1+1 na kraj i tako dok ne dodjemo do jedinstvenog
            {
                ifExists = false;
                if (db.AdminUserRepository.Get(x => x.UserName == username).FirstOrDefault() != null) // radimo proveru da li username vec postoji!!!
                {
                    username = String.Concat(adminDto.FirstName.ToLower(), ".", adminDto.LastName.ToLower(), i++);
                    ifExists = true;
                }
            }
            while (ifExists == true);

            return username;
        }
       
        public static void GradeMailSender(SemesterGrade grade)
        {
            Student student = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent.Student;
            SchoolClass schoolClass = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.SchoolClass;
            Teacher teacher = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.TeacherTeachSubject.Teacher;
            Subject subject = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.TeacherTeachSubject.Subject;
            string mailSubject = String.Format("Dodela ocene za ucenika {0} {1}", student.FirstName, student.LastName);
            string body = String.Format("<div>" +
                            "<h4>Nova ocena!</h4>" +
                            "<p> Ucenik {0} {1} dobio je ocenu: {2} </p>" +
                            "<p> Predmet: {3} </p>" +      
                            "<p> Predmetni nastavnik: {4} {5} </p>" +          
                            "<hr>" +
                            "<p> Za sve dodatne informacije obratiti se razrednom staresini: {6} {7} </p>" +            
                          "</div> ", student.FirstName, student.LastName, (int)grade.Mark, subject.Name, teacher.FirstName, teacher.LastName, schoolClass.HeadTeacher.FirstName, schoolClass.HeadTeacher.LastName);
            string FromMail = ConfigurationManager.AppSettings["from"];
            string emailTo = student.Parent.Email;//"ivan.vasiljevic@hotmail.com";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = mailSubject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["password"]);
            SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpSsl"]);
            SmtpServer.Send(mail);
        }

        public static void FinaleSemesterGradeMailSender(FinaleSemesterGrade grade)
        {
            Student student = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent.Student;
            SchoolClass schoolClass = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.SchoolClass;
            Teacher teacher = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.TeacherTeachSubject.Teacher;
            Subject subject = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.TeacherTeachSubject.Subject;
            Semester semester = grade.TeacherTeachSubjectToSchoolClassToStudentAtSemester.Semester;
            string mailSubject = String.Format("Dodela ZAKLJUCNE semestralne ocene za ucenika {0} {1}", student.FirstName, student.LastName);
            string body = String.Format("<div>" +
                            "<h4>ZAKLJUCNA ocena!</h4>" +
                            "<p> Ucenik {0} {1} dobio je zakljucnu ocenu: {2} </p>" +
                            "<p> Predmet: {3} </p>" +
                            "<p> Semestar: {4} </p>" +
                            "<p> Predmetni nastavnik: {5} {6} </p>" +
                            "<hr>" +
                            "<p> Za sve dodatne informacije obratiti se razrednom staresini: {7} {8} </p>" +
                          "</div> ", student.FirstName, student.LastName, (int)grade.Mark, subject.Name, (int)semester.Name, teacher.FirstName, teacher.LastName, schoolClass.HeadTeacher.FirstName, schoolClass.HeadTeacher.LastName);
            string FromMail = ConfigurationManager.AppSettings["from"];
            string emailTo = student.Parent.Email;//"ivan.vasiljevic@hotmail.com";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = mailSubject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["password"]);
            SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpSsl"]);
            SmtpServer.Send(mail);
        }

        public static void FinaleGradeMailSender(FinaleGrade grade)
        {
            Student student = grade.TeacherTeachSubjectToSchoolClassToStudent.Student;
            SchoolClass schoolClass = grade.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.SchoolClass;
            Teacher teacher = grade.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.TeacherTeachSubject.Teacher;
            Subject subject = grade.TeacherTeachSubjectToSchoolClassToStudent.TeacherTeachSubjectToSchoolClass.TeacherTeachSubject.Subject;
            string mailSubject = String.Format("Dodela ZAKLJUCNE ocene za ucenika {0} {1}", student.FirstName, student.LastName);
            string body = String.Format("<div>" +
                            "<h4>ZAKLJUCNA ocena!</h4>" +
                            "<p> Ucenik {0} {1} dobio je zakljucnu ocenu: {2} </p>" +
                            "<p> Predmet: {3} </p>" +
                            "<p> Predmetni nastavnik: {4} {5} </p>" +
                            "<hr>" +
                            "<p> Za sve dodatne informacije obratiti se razrednom staresini: {6} {7} </p>" +
                          "</div> ", student.FirstName, student.LastName, (int)grade.Mark, subject.Name, teacher.FirstName, teacher.LastName, schoolClass.HeadTeacher.FirstName, schoolClass.HeadTeacher.LastName);
            string FromMail = ConfigurationManager.AppSettings["from"];
            string emailTo = student.Parent.Email;//"ivan.vasiljevic@hotmail.com";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
            mail.From = new MailAddress(FromMail);
            mail.To.Add(emailTo);
            mail.Subject = mailSubject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["password"]);
            SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpSsl"]);
            SmtpServer.Send(mail);
        }
    }
}