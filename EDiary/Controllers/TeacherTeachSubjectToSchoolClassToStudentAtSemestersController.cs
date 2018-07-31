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
    [RoutePrefix("ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters")]
    public class TeacherTeachSubjectToSchoolClassToStudentAtSemestersController : ApiController
    {
        private ITeacherTeachSubjectToSchoolClassToStudentAtSemestersService service;

        public TeacherTeachSubjectToSchoolClassToStudentAtSemestersController(ITeacherTeachSubjectToSchoolClassToStudentAtSemestersService service)
        {
            this.service = service;
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherTeachSubjectToSchoolClassToStudentAtSemester> GetTeacherTeachSubjectToSchoolClassToStudentAtSemesters()
        {
            return service.GetTeacherTeachSubjectToSchoolClassToStudentAtSemesters();
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters/5
        [Route("{id}")]
        [ResponseType(typeof(TeacherTeachSubjectToSchoolClassToStudentAtSemester))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetTeacherTeachSubjectToSchoolClassToStudentAtSemesterById(int id)
        {
            TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester = service.GetTeacherTeachSubjectToSchoolClassToStudentAtSemesterById(id);
            if (teacherTeachSubjectToSchoolClassToStudentAtSemester == null)
            {
                return NotFound();
            }

            return Ok(teacherTeachSubjectToSchoolClassToStudentAtSemester);
        }

        //// POST: ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters
        //[Route("")]
        //[ResponseType(typeof(TeacherTeachSubjectToSchoolClassToStudentAtSemester))]
        //public IHttpActionResult PostTeacherTeachSubjectToSchoolClassToStudentAtSemester(TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    TeacherTeachSubjectToSchoolClassToStudentAtSemester postedTeacherTeachSubjectToSchoolClassToStudentAtSemester = service.PostTeacherTeachSubjectToSchoolClassToStudentAtSemester(teacherTeachSubjectToSchoolClassToStudentAtSemester);

        //    if (postedTeacherTeachSubjectToSchoolClassToStudentAtSemester == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Created("", postedTeacherTeachSubjectToSchoolClassToStudentAtSemester);
        //}

        //// PUT: ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters/5
        //[Route("{id}")]
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id, TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    bool done = service.PutTeacherTeachSubjectToSchoolClassToStudentAtSemester(id, teacherTeachSubjectToSchoolClassToStudentAtSemester);
        //    if (done == false)
        //    {
        //        return BadRequest();
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // DELETE: ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters/5
        [Route("{id}")]
        [ResponseType(typeof(TeacherTeachSubjectToSchoolClassToStudentAtSemester))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteTeacherTeachSubjectToSchoolClassToStudentAtSemester(int id)
        {
            TeacherTeachSubjectToSchoolClassToStudentAtSemester teacherTeachSubjectToSchoolClassToStudentAtSemester = service.DeleteTeacherTeachSubjectToSchoolClassToStudentAtSemester(id);
            if (teacherTeachSubjectToSchoolClassToStudentAtSemester == null)
            {
                return NotFound();
            }

            return Ok(teacherTeachSubjectToSchoolClassToStudentAtSemester);
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters/teacherTeachSubjectToSchoolClassToStudent/5/teacherTeachSubjectToSchoolClassToStudentAtSemesters
        [Route("teacherTeachSubjectToSchoolClassToStudent/{id}/teacherTeachSubjectToSchoolClassToStudentAtSemesters")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherTeachSubjectToSchoolClassToStudentAtSemester> GetTeacherTeachSubjectToSchoolClassToStudentAtSemesterByTeacherTeachSubjectToSchoolClassToStudent(int id)
        {
            IEnumerable<TeacherTeachSubjectToSchoolClassToStudentAtSemester> teacherTeachSubjectToSchoolClassToStudentAtSemesters = service.GetTeacherTeachSubjectToSchoolClassToStudentAtSemesterByTeacherTeachSubjectToSchoolClassToStudent(id);

            return teacherTeachSubjectToSchoolClassToStudentAtSemesters;
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters/semester/SECOND/teacherTeachSubjectToSchoolClassToStudents
        [Route("semester/{id}/teacherTeachSubjectToSchoolClassToStudents")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherTeachSubjectToSchoolClassToStudent> GetTeacherTeachSubjectToSchoolClassToStudentsBySemester(SemesterEnum id)
        {
            IEnumerable<TeacherTeachSubjectToSchoolClassToStudent> teacherTeachSubjectToSchoolClassToStudents = service.GetTeacherTeachSubjectToSchoolClassToStudentsBySemester(id);

            return teacherTeachSubjectToSchoolClassToStudents;
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudentAtSemesters/5/semesterGrades
        [Route("{id}/semesterGrades")]
        [Authorize(Roles = "admins")]
        public IEnumerable<SemesterGrade> GetSemesterGrades(int id)
        {
            IEnumerable<SemesterGrade> semesterGrades = service.GetSemesterGrades(id);

            return semesterGrades;
        }
    }
}
