using EDiary.Infrastructure;
using EDiary.Models;
using EDiary.Providers;
using EDiary.Repositories;
using EDiary.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

[assembly: OwinStartup(typeof(EDiary.Startup))]
namespace EDiary
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = SetupUnity();
            ConfigureOAuth(app, container); // OAuth mora biti registrovan (konfigurisan) pre WebApi-ja!!!  

            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new UnityDependencyResolver(container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            WebApiConfig.Register(config);
            app.UseWebApi(config);          
        }

        public void ConfigureOAuth(IAppBuilder app, UnityContainer container)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(container)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        private UnityContainer SetupUnity()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAdminUsersService, AdminUsersService>();
            container.RegisterType<IStudentsService, StudentsService>();
            container.RegisterType<ITeachersService, TeachersService>();
            container.RegisterType<IParentsService, ParentsService>();
            container.RegisterType<ISubjectsService, SubjectsService>();
            container.RegisterType<ILessonsService, LessonsService>();
            container.RegisterType<ILecturesService, LecturesService>();
            container.RegisterType<ISchoolClassesService, SchoolClassesService>();
            container.RegisterType<ISemestersService, SemestersService>();
            container.RegisterType<ISemesterGradesService, SemesterGradesService>();
            container.RegisterType<IFinaleSemesterGradesService, FinaleSemesterGradesService>();
            container.RegisterType<IFinaleGradesService, FinaleGradesService>();
            container.RegisterType<IStudentsAbsencesService, StudentsAbsencesService>();
            container.RegisterType<ITeacherTeachSubjectsService, TeacherTeachSubjectsService>();
            container.RegisterType<ITeacherTeachSubjectToSchoolClassesService, TeacherTeachSubjectToSchoolClassesService>();
            container.RegisterType<ITeacherTeachSubjectToSchoolClassToStudentsService, TeacherTeachSubjectToSchoolClassToStudentsService>();
            container.RegisterType<ITeacherTeachSubjectToSchoolClassToStudentAtSemestersService, TeacherTeachSubjectToSchoolClassToStudentAtSemestersService>();

            container.RegisterType<DbContext, AuthContext>(new HierarchicalLifetimeManager());
            
            container.RegisterType<IAuthRepository, AuthRepository>();
            //container.RegisterType<IUserService, UserService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IGenericRepository<User>, GenericRepository<User>>();
            container.RegisterType<IGenericRepository<AdminUser>, GenericRepository<AdminUser>>();
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


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
          
            return container;
        }
    }

}