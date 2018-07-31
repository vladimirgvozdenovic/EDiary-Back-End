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
    [RoutePrefix("ediary/teacherTeachSubjectToSchoolClassToStudents")]
    public class TeacherTeachSubjectToSchoolClassToStudentsController : ApiController
    {
        private ITeacherTeachSubjectToSchoolClassToStudentsService service;

        public TeacherTeachSubjectToSchoolClassToStudentsController(ITeacherTeachSubjectToSchoolClassToStudentsService service)
        {
            this.service = service;
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudents
        [Route("")]
        [Authorize(Roles = "admins, teachers")]
        public IEnumerable<TeacherTeachSubjectToSchoolClassToStudent> GetTeacherTeachSubjectToSchoolClassToStudents()
        {
            return service.GetTeacherTeachSubjectToSchoolClassToStudents();
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudents/5
        [Route("{id}")]
        [ResponseType(typeof(TeacherTeachSubjectToSchoolClassToStudent))]
        [Authorize(Roles = "admins, teachers, students, parents")]
        public IHttpActionResult GetTeacherTeachSubjectToSchoolClassToStudentById(int id)
        {
            TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent = service.GetTeacherTeachSubjectToSchoolClassToStudentById(id);
            if (teacherTeachSubjectToSchoolClassToStudent == null)
            {
                return NotFound();
            }

            return Ok(teacherTeachSubjectToSchoolClassToStudent);
        }

        //// POST: ediary/teacherTeachSubjectToSchoolClassToStudents
        //[Route("")]
        //[ResponseType(typeof(TeacherTeachSubjectToSchoolClassToStudent))]
        //public IHttpActionResult PostTeacherTeachSubjectToSchoolClassToStudent(TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    TeacherTeachSubjectToSchoolClassToStudent postedTeacherTeachSubjectToSchoolClassToStudent = service.PostTeacherTeachSubjectToSchoolClassToStudent(teacherTeachSubjectToSchoolClassToStudent);

        //    if (postedTeacherTeachSubjectToSchoolClassToStudent == null)
        //    {
        //        return BadRequest();
        //    }

        //    return Created("", postedTeacherTeachSubjectToSchoolClassToStudent);
        //}

        //// PUT: ediary/teacherTeachSubjectToSchoolClassToStudents/5
        //[Route("{id}")]
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutTeacherTeachSubjectToSchoolClassToStudent(int id, TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    bool done = service.PutTeacherTeachSubjectToSchoolClassToStudent(id, teacherTeachSubjectToSchoolClassToStudent);
        //    if (done == false)
        //    {
        //        return BadRequest();
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // DELETE: ediary/teacherTeachSubjectToSchoolClassToStudents/5
        [Route("{id}")]
        [ResponseType(typeof(TeacherTeachSubjectToSchoolClassToStudent))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteTeacherTeachSubjectToSchoolClassToStudent(int id)
        {
            TeacherTeachSubjectToSchoolClassToStudent teacherTeachSubjectToSchoolClassToStudent = service.DeleteTeacherTeachSubjectToSchoolClassToStudent(id);
            if (teacherTeachSubjectToSchoolClassToStudent == null)
            {
                return NotFound();
            }

            return Ok(teacherTeachSubjectToSchoolClassToStudent);
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudents/teacherSubjects/5
        [Route("student/{id}/teacherTeachSubjectsToSchoolClasses")]
        [Authorize(Roles = "admins, students")]
        public IEnumerable<TeacherTeachSubjectToSchoolClass> GetTeacherTeachSubjectsToSchoolClassesByStudent(string id)
        {
            IEnumerable<TeacherTeachSubjectToSchoolClass> teacherTeachSubjectToSchoolClasses = service.GetTeacherTeachSubjectsToSchoolClassesByStudent(id);

            return teacherTeachSubjectToSchoolClasses;
        }

        // GET: ediary/teacherTeachSubjectToSchoolClassToStudents/subjectTeachers/5
        [Route("teacherTeachSubjectsToSchoolClass/{id}/students")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Student> GetStudentsByTeacherTeachSubjectToSchoolClass(int id)
        {
            IEnumerable<Student> students = service.GetStudentsByTeacherTeachSubjectToSchoolClass(id);

            return students;
        }
    }
}
