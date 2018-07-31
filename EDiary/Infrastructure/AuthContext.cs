using EDiary.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EDiary.Infrastructure
{
    public class AuthContext : IdentityDbContext<User>   // DataAccessContext se vise ne koristi (dodavanjem Startup.cs je iskljucen Global.asax (moze i da se obrise))
    {
        public AuthContext() : base("DataAccessConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AuthContext>());
        }

        public DbSet<AdminUser> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SemesterGrade> SemesterGrades { get; set; }
        public DbSet<FinaleSemesterGrade> FinaleSemesterGrades { get; set; }
        public DbSet<FinaleGrade> FinaleGrades { get; set; }
        public DbSet<StudentsAbsence> StudentsAbsences { get; set; }
        public DbSet<TeacherTeachSubject> TeacherTeachSubjects { get; set; }
        public DbSet<TeacherTeachSubjectToSchoolClass> TeacherTeachSubjectToSchoolClasses { get; set; }
        public DbSet<TeacherTeachSubjectToSchoolClassToStudent> TeacherTeachSubjectToSchoolClassToStudents { get; set; }
        public DbSet<TeacherTeachSubjectToSchoolClassToStudentAtSemester> TeacherTeachSubjectToSchoolClassToStudentAtSemesters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().ToTable("Students");  // ovo je bitno!!! (FluentApi)
            modelBuilder.Entity<Teacher>().ToTable("Teachers"); // ovo je bitno!!! (FluentApi)
            modelBuilder.Entity<Parent>().ToTable("Parents"); // ovo je bitno!!! (FluentApi)
            modelBuilder.Entity<AdminUser>().ToTable("Admins"); // ovo je bitno!!! (FluentApi)

            //IZGLEDA DA NE MOZE SA ANOTACIJOM U SLUCAJU OAUTH2!!!!
            // Ovo gore sam resio dodavanjem anotacije u modelu (Bez FluentApi)

            // [Table("Customer")]
            // public class Customer : User
            // {
            // }

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();  // Ovako se u potpunosti iskljucuje kaskadno brisanje svih entiteta

            //modelBuilder.Entity<Subject>().     // Ovde iskljucujemo kaskadno brisanje izmedju entiteta "Subject" i "TeacherTeachSubjectToSchoolClasses"
            //    HasMany(p => p.TeacherTeachSubjectToSchoolClasses).
            //    WithRequired(a => a.Subject).
            //    WillCascadeOnDelete(false);

            modelBuilder.Entity<TeacherTeachSubject>().     // Ovde iskljucujemo kaskadno brisanje izmedju entiteta "TeacherTeachSubject" i "TeacherTeachSubjectToSchoolClasses"
               HasMany(p => p.TeacherTeachSubjectToSchoolClasses).
               WithRequired(a => a.TeacherTeachSubject).
               WillCascadeOnDelete(false);

            modelBuilder.Entity<TeacherTeachSubjectToSchoolClassToStudent>()    // Ovde mapiramo vezu 1:1 izmedju entiteta "TeacherTeachSubjectToSchoolClassToStudent" i "FinaleGrade"
                .HasOptional(s => s.FinaleGrade)
                .WithRequired(ad => ad.TeacherTeachSubjectToSchoolClassToStudent)
                .Map(m => m.MapKey("TeacherTeachSubjectToSchoolClassToStudentId"));   // Bez mapiranja kljuca ne bi prikazivao kolonu "TeacherTeachSubjectToSchoolClassToStudentId" u tabeli "dbo.FinaleGrades"

            modelBuilder.Entity<TeacherTeachSubjectToSchoolClassToStudentAtSemester>()    // Ovde mapiramo vezu 1:1 izmedju entiteta "TeacherTeachSubjectToSchoolClassToStudentAtSemester" i "FinaleSemesterGrade"
                .HasOptional(s => s.FinaleSemesterGrade)
                .WithRequired(ad => ad.TeacherTeachSubjectToSchoolClassToStudentAtSemester)
                .Map(m => m.MapKey("TeacherTeachSubjectToSchoolClassToStudentAtSemesterId"));    // Bez mapiranja kljuca ne bi prikazivao kolonu "TeacherTeachSubjectToSchoolClassToStudentAtSemesterId" u tabeli "dbo.FinaleSemesterGrades"

            modelBuilder.Entity<Teacher>()    // Ovde mapiramo vezu 1:1 izmedju entiteta "Teacher" i "SchoolClass"
                .HasOptional(s => s.HeadClass)
                .WithRequired(ad => ad.HeadTeacher)
                .Map(m => m.MapKey("HeadTeacherId"));    // Bez mapiranja kljuca ne bi prikazivao kolonu "TeacherId" u tabeli "dbo.SchoolClasses"

        }
    }
}