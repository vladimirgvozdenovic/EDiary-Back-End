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
    [RoutePrefix("ediary/finaleGrades")]
    public class FinaleGradesController : ApiController
    {
        private IFinaleGradesService service;

        public FinaleGradesController(IFinaleGradesService service)
        {
            this.service = service;
        }

        // GET: ediary/finaleGrades
        [Route("")]
        [Authorize(Roles = "admins, teachers")]
        public IEnumerable<FinaleGrade> GetFinaleGrades()
        {
            return service.GetFinaleGrades();
        }

        // GET: ediary/finaleGrades/5
        [Route("{id}")]
        [ResponseType(typeof(FinaleGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult GetFinaleGradeById(int id)
        {
            FinaleGrade finaleGrade = service.GetFinaleGradeById(id);
            if (finaleGrade == null)
            {
                return NotFound();
            }

            return Ok(finaleGrade);
        }

        // POST: ediary/finaleGrades
        [Route("")]
        [ResponseType(typeof(FinaleGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult PostFinaleGrade(FinaleGradeDTO finaleGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FinaleGrade postedFinaleGrade = service.PostFinaleGrade(finaleGradeDto);

            if (postedFinaleGrade == null)
            {
                return BadRequest();
            }

            return Created("", postedFinaleGrade);
        }

        // PUT: ediary/finaleGrades/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutFinaleGrade(int id, FinaleGradeDTO finaleGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutFinaleGrade(id, finaleGradeDto);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/finaleGrades/5
        [Route("{id}")]
        [ResponseType(typeof(FinaleGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult DeleteFinaleGrade(int id)
        {
            FinaleGrade finaleGrade = service.DeleteFinaleGrade(id);
            if (finaleGrade == null)
            {
                return NotFound();
            }

            return Ok(finaleGrade);
        }

        // GET: ediary/finaleGrades/teacherTeachSubjectToSchoolClassToStudent/5/finaleGrades
        [Route("finaleGradesByDate/{date}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<FinaleGrade> GetFinaleGradesByDate(DateTime date)
        {
            IEnumerable<FinaleGrade> finaleGrades = service.GetFinaleGradesByDate(date);

            return finaleGrades;
        }
    }
}
