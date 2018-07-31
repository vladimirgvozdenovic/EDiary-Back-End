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
    [RoutePrefix("ediary/lectures")]
    public class LecturesController : ApiController
    {
        private ILecturesService service;

        public LecturesController(ILecturesService service)
        {
            this.service = service;
        }

        // GET: ediary/lectures
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Lecture> GetLectures()
        {
            return service.GetLectures();
        }

        // GET: ediary/lectures/5
        [Route("{id}")]
        [ResponseType(typeof(Lecture))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetLectureById(int id)
        {
            Lecture lecture = service.GetLectureById(id);
            if (lecture == null)
            {
                return NotFound();
            }

            return Ok(lecture);
        }

        // POST: ediary/lectures
        [Route("")]
        [ResponseType(typeof(Lecture))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostLecture(Lecture lecture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lecture postedLecture = service.PostLecture(lecture);

            if (postedLecture == null)
            {
                return BadRequest();
            }

            return Created("", postedLecture);
        }

        // PUT: ediary/lectures/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutLecture(int id, Lecture lecture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutLecture(id, lecture);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/lectures/5
        [Route("{id}")]
        [ResponseType(typeof(Lecture))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteLecture(int id)
        {
            Lecture lecture = service.DeleteLecture(id);
            if (lecture == null)
            {
                return NotFound();
            }

            return Ok(lecture);
        }

        // GET: ediary/lectures/lecturesByDate/2018-07-11
        [Route("lecturesByDate/{date}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Lecture> GetLecturesByDate(DateTime date)
        {
            IEnumerable<Lecture> lectures = service.GetLecturesByDate(date);

            return lectures;
        }

        // GET: ediary/lectures/byTeacherTeachSubjectToSchoolClass/5
        [Route("byTeacherTeachSubjectToSchoolClass/{id}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Lecture> GetLecturesByTeacherTeachSubjectToSchoolClass(int id)
        {
            IEnumerable<Lecture> lectures = service.GetLecturesByTeacherTeachSubjectToSchoolClass(id);

            return lectures;
        }
    }
}
