using EDiary.Infrastructure;
using EDiary.Models;
using EDiary.Repositories;
using EDiary.Services;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace EDiary
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IStudentsService, StudentsService>();
            container.RegisterType<ITeachersService, TeachersService>();
            container.RegisterType<IParentsService, ParentsService>();
            //container.RegisterType<ISubjectsService, SubjectsService>();
            //container.RegisterType<ILessonsService, LessonsService>();
            //container.RegisterType<ILecturesService, LecturesService>();
            container.RegisterType<ISchoolClassesService, SchoolClassesService>();
            //container.RegisterType<ISemestersService, SemestersService>();
            //container.RegisterType<ISemesterGradesService, SemesterGradesService>();
            //container.RegisterType<IFinaleSemesterGradesService, FinaleSemesterGradesService>();
            //container.RegisterType<IFinaleGradesService, FinaleGradesService>();
            //container.RegisterType<IStudentsAbsencesService, StudentsAbsencesService>();
            //container.RegisterType<ITeacherTeachSubjectsService, TeacherTeachSubjectsService>();
            //container.RegisterType<ITeacherTeachSubjectToSchoolClassesService, TeacherTeachSubjectToSchoolClassesService>();
            //container.RegisterType<ITeacherTeachSubjectToSchoolClassToStudentsService, TeacherTeachSubjectToSchoolClassToStudentsService>();
            //container.RegisterType<ITeacherTeachSubjectToSchoolClassToStudentAtSemestersService, TeacherTeachSubjectToSchoolClassToStudentAtSemestersService>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IGenericRepository<Student>, GenericRepository<Student>>();
            container.RegisterType<IGenericRepository<Teacher>, GenericRepository<Teacher>>();
            container.RegisterType<IGenericRepository<Parent>, GenericRepository<Parent>>();
            container.RegisterType<IGenericRepository<Subject>, GenericRepository<Subject>>();
            container.RegisterType<IGenericRepository<Lesson>, GenericRepository<Lesson>>();
            container.RegisterType<IGenericRepository<Lecture>, GenericRepository<Lecture>>();
            container.RegisterType<IGenericRepository<SchoolClass>, GenericRepository<SchoolClass>>();
            container.RegisterType<IGenericRepository<Semester>, GenericRepository<Semester>>();
            container.RegisterType<IGenericRepository<SemesterGrade>, GenericRepository<SemesterGrade>>();
            container.RegisterType<IGenericRepository<FinaleSemesterGrade>, GenericRepository<FinaleSemesterGrade>>();
            container.RegisterType<IGenericRepository<FinaleGrade>, GenericRepository<FinaleGrade>>();
            container.RegisterType<IGenericRepository<StudentsAbsence>, GenericRepository<StudentsAbsence>>();
            container.RegisterType<IGenericRepository<TeacherTeachSubject>, GenericRepository<TeacherTeachSubject>>();
            container.RegisterType<IGenericRepository<TeacherTeachSubjectToSchoolClass>, GenericRepository<TeacherTeachSubjectToSchoolClass>>();
            container.RegisterType<IGenericRepository<TeacherTeachSubjectToSchoolClassToStudent>, GenericRepository<TeacherTeachSubjectToSchoolClassToStudent>>();
            container.RegisterType<IGenericRepository<TeacherTeachSubjectToSchoolClassToStudentAtSemester>, GenericRepository<TeacherTeachSubjectToSchoolClassToStudentAtSemester>>();

            container.RegisterType<DbContext, /*DataAccessContext*/AuthContext>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}