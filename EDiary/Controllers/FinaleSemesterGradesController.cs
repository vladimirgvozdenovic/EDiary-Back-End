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
    [RoutePrefix("ediary/finaleSemesterGrades")]
    public class FinaleSemesterGradesController : ApiController
    {
        private IFinaleSemesterGradesService service;

        public FinaleSemesterGradesController(IFinaleSemesterGradesService service)
        {
            this.service = service;
        }

        // GET: ediary/finaleSemesterGrades
        [Route("")]
        [Authorize(Roles = "admins, teachers")]
        public IEnumerable<FinaleSemesterGrade> GetFinaleSemesterGrades()
        {
            return service.GetFinaleSemesterGrades();
        }

        // GET: ediary/finaleSemesterGrades/5
        [Route("{id}")]
        [ResponseType(typeof(FinaleSemesterGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult GetFinaleSemesterGradeById(int id)
        {
            FinaleSemesterGrade finaleSemesterGrade = service.GetFinaleSemesterGradeById(id);
            if (finaleSemesterGrade == null)
            {
                return NotFound();
            }

            return Ok(finaleSemesterGrade);
        }

        // POST: ediary/finaleSemesterGrades
        [Route("")]
        [ResponseType(typeof(FinaleSemesterGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult PostFinaleSemesterGrade(FinaleSemesterGradeDTO finaleSemesterGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FinaleSemesterGrade postedFinaleSemesterGrade = service.PostFinaleSemesterGrade(finaleSemesterGradeDto);

            if (postedFinaleSemesterGrade == null)
            {
                return BadRequest();
            }

            return Created("", postedFinaleSemesterGrade);
        }

        // PUT: ediary/finaleSemesterGrades/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutFinaleSemesterGrade(int id, FinaleSemesterGradeDTO finaleSemesterGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutFinaleSemesterGrade(id, finaleSemesterGradeDto);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/finaleSemesterGrades/5
        [Route("{id}")]
        [ResponseType(typeof(FinaleSemesterGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult DeleteFinaleSemesterGrade(int id)
        {
            FinaleSemesterGrade finaleSemesterGrade = service.DeleteFinaleSemesterGrade(id);
            if (finaleSemesterGrade == null)
            {
                return NotFound();
            }

            return Ok(finaleSemesterGrade);
        }

        // GET: ediary/finaleSemesterGrades/finaleSemesterGradesByDate/2018-07-11
        [Route("finaleSemesterGradesByDate/{date}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<FinaleSemesterGrade> GetFinaleSemesterGradesByDate(DateTime date)
        {
            IEnumerable<FinaleSemesterGrade> finaleSemesterGrades = service.GetFinaleSemesterGradesByDate(date);

            return finaleSemesterGrades;
        }
    }
}
