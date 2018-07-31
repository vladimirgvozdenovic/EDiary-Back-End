using EDiary.Models;
using EDiary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EDiary.Controllers
{
    [RoutePrefix("ediary/teacherTeachSubjectToSchoolClasses")]
    public class TeacherTeachSubjectToSchoolClassesController : ApiController
    {
        private ITeacherTeachSubjectToSchoolClassesService service;

        public TeacherTeachSubjectToSchoolClassesController(ITeacherTeachSubjectToSchoolClassesService service)
        {
            this.service = service;
        }

        // GET: ediary/teacherTeachSubjectToSchoolClasses
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherTeachSubjectToSchoolClass> GetTeacherTeachSubjectToSchoolClasses()
        {
            return service.GetTeacherTeachSubjectToSchoolClasses();
        }

        // GET: ediary/teacherTeachSubjectToSchoolClasses/5
        [Route("{id}")]
        [ResponseType(typeof(TeacherTeachSubjectToSchoolClass))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetTeacherTeachSubjectToSchoolClassById(int id)
        {
            TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass = service.GetTeacherTeachSubjectToSchoolClassById(id);
            if (teacherTeachSubjectToSchoolClass == null)
            {
                return NotFound();
            }

            return Ok(teacherTeachSubjectToSchoolClass);
        }

        // POST: ediary/teacherTeachSubjectToSchoolClasses
        [Route("")]
        [ResponseType(typeof(TeacherTeachSubjectToSchoolClass))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostTeacherTeachSubjectToSchoolClass(TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TeacherTeachSubjectToSchoolClass postedTeacherTeachSubjectToSchoolClass = service.PostTeacherTeachSubjectToSchoolClass(teacherTeachSubjectToSchoolClass);

            if (postedTeacherTeachSubjectToSchoolClass == null)
            {
                return BadRequest();
            }

            return Created("", postedTeacherTeachSubjectToSchoolClass);
        }

        // PUT: ediary/teacherTeachSubjectToSchoolClasses/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutTeacherTeachSubjectToSchoolClass(int id, TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutTeacherTeachSubjectToSchoolClass(id, teacherTeachSubjectToSchoolClass);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/teacherTeachSubjectToSchoolClasses/5
        [Route("{id}")]
        [ResponseType(typeof(TeacherTeachSubjectToSchoolClass))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteTeacherTeachSubjectToSchoolClass(int id)
        {
            TeacherTeachSubjectToSchoolClass teacherTeachSubjectToSchoolClass = service.DeleteTeacherTeachSubjectToSchoolClass(id);
            if (teacherTeachSubjectToSchoolClass == null)
            {
                return NotFound();
            }

            return Ok(teacherTeachSubjectToSchoolClass);
        }

        // GET: ediary/teacherTeachSubjectToSchoolClasses/teacherSubjects/5
        [Route("schoolClass/{id}/teacherTeachSubjects")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherTeachSubject> GetTeacherTeachSubjectsBySchoolClass(string id)
        {
            IEnumerable<TeacherTeachSubject> teacherTeachSubjects = service.GetTeacherTeachSubjectsBySchoolClass(id);

            return teacherTeachSubjects;
        }

        // GET: ediary/teacherTeachSubjectToSchoolClasses/subjectTeachers/5
        [Route("teacherTeachSubject/{id}/schoolClasses")]
        [Authorize(Roles = "admins")]
        public IEnumerable<SchoolClass> GetSchoolClassesByTeacherTeachSubject(int id)
        {
            IEnumerable<SchoolClass> schoolClasses = service.GetSchoolClassesByTeacherTeachSubject(id);

            return schoolClasses;
        }
    }
}
