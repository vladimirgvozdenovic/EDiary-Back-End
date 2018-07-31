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
    [RoutePrefix("ediary/semesterGrades")]
    public class SemesterGradesController : ApiController
    {
        private ISemesterGradesService service;

        public SemesterGradesController(ISemesterGradesService service)
        {
            this.service = service;
        }

        // GET: ediary/semesterGrades
        [Route("")]
        [Authorize(Roles = "admins, teachers")]
        public IEnumerable<SemesterGrade> GetSemesterGrades()
        {
            return service.GetSemesterGrades();
        }

        // GET: ediary/semesterGrades/5
        [Route("{id}")]
        [ResponseType(typeof(SemesterGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult GetSemesterGradeById(int id)
        {
            SemesterGrade semesterGrade = service.GetSemesterGradeById(id);
            if (semesterGrade == null)
            {
                return NotFound();
            }

            return Ok(semesterGrade);
        }

        // POST: ediary/semesterGrades
        [Route("")]
        [ResponseType(typeof(SemesterGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult PostSemesterGrade(SemesterGradeDTO semesterGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SemesterGrade postedSemesterGrade = service.PostSemesterGrade(semesterGradeDto);

            if (postedSemesterGrade == null)
            {
                return BadRequest();
            }

            return Created("", postedSemesterGrade);
        }

        // PUT: ediary/semesterGrades/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutSemesterGrade(int id, SemesterGradeDTO semesterGradeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutSemesterGrade(id, semesterGradeDto);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/semesterGrades/5
        [Route("{id}")]
        [ResponseType(typeof(SemesterGrade))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult DeleteSemesterGrade(int id)
        {
            SemesterGrade semesterGrade = service.DeleteSemesterGrade(id);
            if (semesterGrade == null)
            {
                return NotFound();
            }

            return Ok(semesterGrade);
        }

        // GET: ediary/semesterGrades/semesterGradesByDate/2018-07-11
        [Route("semesterGradesByDate/{date}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<SemesterGrade> GetSemesterGradesByDate(DateTime date)
        {
            IEnumerable<SemesterGrade> semesterGrades = service.GetSemesterGradesByDate(date);

            return semesterGrades;
        }

        // GET: ediary/semesterGrades/byTeacherTeachSubjectToSchoolClassToStudentAtSemester/5
        [Route("byTeacherTeachSubjectToSchoolClassToStudentAtSemester/{id}")]
        [Authorize(Roles = "admins, teachers")]
        public IEnumerable<SemesterGrade> GetSemesterGradesByTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id)
        {
            IEnumerable<SemesterGrade> semesterGrades = service.GetSemesterGradesByTeacherTeachSubjectToSchoolClassToStudentAtSemester(id);

            return semesterGrades;
        }
    }
}
