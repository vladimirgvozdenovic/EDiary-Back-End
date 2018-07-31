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
    [RoutePrefix("ediary/subjects")]
    public class SubjectsController : ApiController
    {
        private ISubjectsService service;

        public SubjectsController(ISubjectsService service)
        {
            this.service = service;
        }

        // GET: ediary/subjects
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Subject> GetSubjects()
        {
            return service.GetSubjects();
        }

        // GET: ediary/subjects/5
        [Route("{id}")]
        [ResponseType(typeof(Subject))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetSubjectById(int id)
        {
            Subject subject = service.GetSubjectById(id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // POST: ediary/subjects
        [Route("")]
        [ResponseType(typeof(Subject))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subject postedSubject = service.PostSubject(subject);

            if (postedSubject == null)
            {
                return BadRequest();
            }

            return Created("", postedSubject);
        }

        // PUT: ediary/subjects/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutSubject(int id, Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutSubject(id, subject);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/subjects/5
        [Route("{id}")]
        [ResponseType(typeof(Subject))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteSubject(int id)
        {
            Subject subject = service.DeleteSubject(id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // PUT: ediary/subjects/5/lesson/1
        [ResponseType(typeof(Subject))]
        [Route("{id}/lesson/{lessonId}", Name = "AddLesson")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutLesson(int id, int lessonId)
        {
            Subject subject = service.PutLesson(id, lessonId);

            if (subject == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddLesson", new { id = subject.Id, lessonId = lessonId }, subject);
        }

        // GET: ediary/subjects/by-schoolYear/8
        [Route("by-schoolYear/{schoolYear}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Subject> GetSubjectsBySchoolYear(SchoolYearEnum schoolYear)
        {
            return service.GetSubjectsBySchoolYear(schoolYear);
        }

        // GET: ediary/subjects/by-Name/Matematika
        [Route("by-Name/{pattern}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Subject> GetSubjectsByName(string pattern)
        {
            IEnumerable<Subject> subjects = service.GetSubjectsByName(pattern);

            return subjects;
        }

        // GET: ediary/subjects/5/lessons
        [Route("{id}/lessons")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Lesson> GetSubjectLessons(int id)
        {
            IEnumerable<Lesson> lessons = service.GetSubjectLessons(id);
            if (lessons == null)
            {
                return null;
            }

            return lessons;
        }

        // GET: ediary/subjects/{id}/Teachers
        [Route("{id}/teachers")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherDTO> GetSubjectTeachers(int id)
        {
            IEnumerable<TeacherDTO> teachersDto = service.GetSubjectTeachers(id);

            return teachersDto;
        }
    }
}
