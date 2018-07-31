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
    [RoutePrefix("ediary/studentsAbsences")]
    public class StudentsAbsencesController : ApiController
    {
        private IStudentsAbsencesService service;

        public StudentsAbsencesController(IStudentsAbsencesService service)
        {
            this.service = service;
        }

        // GET: ediary/studentsAbsences
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<StudentsAbsence> GetStudentsAbsences()
        {
            return service.GetStudentsAbsences();
        }

        // GET: ediary/studentsAbsences/5
        [Route("{id}")]
        [ResponseType(typeof(StudentsAbsence))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetStudentsAbsenceById(int id)
        {
            StudentsAbsence studentsAbsence = service.GetStudentsAbsenceById(id);
            if (studentsAbsence == null)
            {
                return NotFound();
            }

            return Ok(studentsAbsence);
        }

        // POST: ediary/studentsAbsences
        [Route("")]
        [ResponseType(typeof(StudentsAbsence))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostStudentsAbsence(StudentsAbsence studentsAbsence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StudentsAbsence postedStudentsAbsence = service.PostStudentsAbsence(studentsAbsence);

            if (postedStudentsAbsence == null)
            {
                return BadRequest();
            }

            return Created("", postedStudentsAbsence);
        }

        // PUT: ediary/studentsAbsences/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutStudentsAbsence(int id, StudentsAbsence studentsAbsence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutStudentsAbsence(id, studentsAbsence);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/studentsAbsences/5
        [Route("{id}")]
        [ResponseType(typeof(StudentsAbsence))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteStudentsAbsence(int id)
        {
            StudentsAbsence studentsAbsence = service.DeleteStudentsAbsence(id);
            if (studentsAbsence == null)
            {
                return NotFound();
            }

            return Ok(studentsAbsence);
        }

        // GET: ediary/studentsAbsences/byStudent/3fe448a8-e6db-416d-83de-c7d55703def3
        [Route("byStudent/{id}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<StudentsAbsence> GetStudentsAbsencesByStudent(string id)
        {
            IEnumerable<StudentsAbsence> studentsAbsences = service.GetStudentsAbsencesByStudent(id);

            return studentsAbsences;
        }

        // GET: ediary/studentsAbsences/byLecture/5
        [Route("byLecture/{id}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<StudentsAbsence> GetStudentsAbsencesByLecture(int id)
        {
            IEnumerable<StudentsAbsence> studentsAbsences = service.GetStudentsAbsencesByLecture(id);

            return studentsAbsences;
        }

        // GET: ediary/studentsAbsences/studentsAbsencesByDate/2018-07-11
        [Route("studentsAbsencesByDate/{date}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<StudentsAbsence> GetStudentsAbsencesByDate(DateTime date)
        {
            IEnumerable<StudentsAbsence> studentsAbsences = service.GetStudentsAbsencesByDate(date);

            return studentsAbsences;
        }       
    }
}
