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
    [RoutePrefix("ediary/semesters")]
    public class SemestersController : ApiController
    {
        private ISemestersService service;

        public SemestersController(ISemestersService service)
        {
            this.service = service;
        }

        // GET: ediary/semesters
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Semester> GetSemesters()
        {
            return service.GetSemesters();
        }

        // GET: ediary/semesters/FIRST
        [Route("{id}")]
        [ResponseType(typeof(Semester))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetSemesterById(SemesterEnum id)
        {
            Semester semester = service.GetSemesterById(id);
            if (semester == null)
            {
                return NotFound();
            }

            return Ok(semester);
        }

        // POST: ediary/semesters
        [Route("")]
        [ResponseType(typeof(Semester))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostSemester(Semester semester)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Semester postedSemester = service.PostSemester(semester);

            if (postedSemester == null)
            {
                return BadRequest();
            }

            return Created("", postedSemester);
        }

        // PUT: ediary/semesters/FIRST
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutSemester(SemesterEnum id, Semester semester)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutSemester(id, semester);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/semesters/5
        [Route("{id}")]
        [ResponseType(typeof(Semester))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteSemester(SemesterEnum id)
        {
            Semester semester = service.DeleteSemester(id);
            if (semester == null)
            {
                return NotFound();
            }

            return Ok(semester);
        }
    }
}
