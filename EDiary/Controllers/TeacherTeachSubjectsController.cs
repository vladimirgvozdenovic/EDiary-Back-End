using EDiary.Models;
using EDiary.Models.DTOs;
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
    [RoutePrefix("ediary/teacherTeachSubjects")]
    public class TeacherTeachSubjectsController : ApiController
    {
        private ITeacherTeachSubjectsService service;

        public TeacherTeachSubjectsController(ITeacherTeachSubjectsService service)
        {
            this.service = service;
        }

        // GET: ediary/teacherTeachSubjects
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherTeachSubject> GetTeacherTeachSubjects()
        {
            return service.GetTeacherTeachSubjects();
        }

        // GET: ediary/teacherTeachSubjects/5
        [Route("{id}")]
        [ResponseType(typeof(TeacherTeachSubject))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetTeacherTeachSubjectById(int id)
        {
            TeacherTeachSubject teacherTeachSubject = service.GetTeacherTeachSubjectById(id);
            if (teacherTeachSubject == null)
            {
                return NotFound();
            }

            return Ok(teacherTeachSubject);
        }

        // POST: ediary/teacherTeachSubjects
        [Route("")]
        [ResponseType(typeof(TeacherTeachSubject))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostTeacherTeachSubject(TeacherTeachSubject teacherTeachSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TeacherTeachSubject postedTeacherTeachSubject = service.PostTeacherTeachSubject(teacherTeachSubject);

            if (postedTeacherTeachSubject == null)
            {
                return BadRequest();
            }

            return Created("", postedTeacherTeachSubject);
        }

        // PUT: ediary/teacherTeachSubjects/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutTeacherTeachSubject(int id, TeacherTeachSubject teacherTeachSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutTeacherTeachSubject(id, teacherTeachSubject);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/teacherTeachSubjects/5
        [Route("{id}")]
        [ResponseType(typeof(TeacherTeachSubject))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteTeacherTeachSubject(int id)
        {
            TeacherTeachSubject teacherTeachSubject = service.DeleteTeacherTeachSubject(id);
            if (teacherTeachSubject == null)
            {
                return NotFound();
            }

            return Ok(teacherTeachSubject);
        }

        // GET: ediary/teacherTeachSubjects/teacherSubjects/5
        [Route("teacherSubjects/{id}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Subject> GetSubjectsByTeacher(string id)
        {
            IEnumerable<Subject> subjects = service.GetSubjectsByTeacher(id);

            return subjects;
        }

        // GET: ediary/teacherTeachSubjects/subjectTeachers/5
        [Route("subjectTeachers/{id}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherDTO> GetTeachersBySubject(int id)
        {
            IEnumerable<TeacherDTO> teachersDto = service.GetTeachersBySubject(id);

            return teachersDto;
        }
    }
}
