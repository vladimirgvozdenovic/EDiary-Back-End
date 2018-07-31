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
    [RoutePrefix("ediary/lessons")]
    public class LessonsController : ApiController
    {
        private ILessonsService service;

        public LessonsController(ILessonsService service)
        {
            this.service = service;
        }

        // GET: ediary/lessons
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Lesson> GetLessons()
        {
            return service.GetLessons();
        }

        // GET: ediary/lessons/5
        [Route("{id}")]
        [ResponseType(typeof(Lesson))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetLessonById(int id)
        {
            Lesson lesson = service.GetLessonById(id);
            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(lesson);
        }

        // POST: ediary/lessons
        [Route("")]
        [ResponseType(typeof(Lesson))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostLesson(Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lesson postedLesson = service.PostLesson(lesson);

            if (postedLesson == null)
            {
                return BadRequest();
            }

            return Created("", postedLesson);
        }

        // PUT: ediary/lessons/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutLesson(int id, Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutLesson(id, lesson);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/lessons/5
        [Route("{id}")]
        [ResponseType(typeof(Lesson))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteLesson(int id)
        {
            Lesson lesson = service.DeleteLesson(id);
            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(lesson);
        }

        // PUT: ediary/lessons/5/lesson/1
        [ResponseType(typeof(Lesson))]
        [Route("{id}/subject/{subjectId}", Name = "AddSubject")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutSubject(int id, int subjectId)
        {
            Lesson lesson = service.PutSubject(id, subjectId);

            if (lesson == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddSubject", new { id = lesson.Id, subjectId = lesson.SubjectId }, lesson);
        }

        // GET: ediary/lessons/by-Name/Algebra
        [Route("by-Name/{pattern}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Lesson> GetLessonsByName(string pattern)
        {
            IEnumerable<Lesson> lessons = service.GetLessonsByName(pattern);

            return lessons;
        }

        // GET: ediary/lessons/5/lectures
        [Route("{id}/lectures")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Lecture> GetLessonLectures(int id)
        {
            IEnumerable<Lecture> lectures = service.GetLessonLectures(id);
            if (lectures == null)
            {
                return null;
            }

            return lectures;
        }
    }
}
