using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EDiary.Models
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext() : base("DataAccessConnection")
        {
            Database.SetInitializer<DataAccessContext>(new DropCreateDatabaseIfModelChanges<DataAccessContext>());
        }

        public DbSet<User> Users { get; set; }
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

        //----------------------------------------------------------------------------------------------------------------------------------------
        // Greske koje se javljaju i zbog kojih moramo da iskljucimo kaskadno brisanje izmedju pojedinih entiteta
        //----------------------------------------------------------------------------------------------------------------------------------------

        // 'FK_dbo.TeacherTeachSubjectToSchoolClasses_dbo.Subjects_SubjectId' on table 'TeacherTeachSubjectToSchoolClasses' 
        // may cause cycles or multiple cascade paths.Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        // Could not create constraint or index.See previous errors.'

        // 'FK_dbo.TeacherTeachSubjectToSchoolClasses_dbo.TeacherTeachSubjects_TeacherTeachSubjectId' on table 'TeacherTeachSubjectToSchoolClasses' 
        // may cause cycles or multiple cascade paths.Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        // Could not create constraint or index.See previous errors.'

        // 'FK_dbo.TeacherTeachSubjectToSchoolClassToStudentAtSemesters_dbo.TeacherTeachSubjectToSchoolClassToStudents_TeacherTeachSubjectToSchoolClass' on table 'TeacherTeachSubjectToSchoolClassToStudentAtSemesters' 
        // may cause cycles or multiple cascade paths.Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        // Could not create constraint or index.See previous errors.'

        //----------------------------------------------------------------------------------------------------------------------------------------
        // Greske koje se javljaju i zbog kojih moramo da mapiramo veze 1:1
        //----------------------------------------------------------------------------------------------------------------------------------------

        // 'Unable to determine the principal end of an association between the types 
        // 'EDiary.Models.TeacherTeachSubjectToSchoolClassToStudent' and 'EDiary.Models.FinaleGrade'. 
        // The principal end of this association must be explicitly configured using either the relationship fluent API or data annotations.'

    }
}