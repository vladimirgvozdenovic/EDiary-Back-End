using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Repositories;
using EDiary.Services;
using EDiary.Utilities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace EDiary.Controllers
{
    [RoutePrefix("ediary/students")]
    public class StudentsController : ApiController
    {
        private IStudentsService service;

        public StudentsController(IStudentsService service)
        {
            this.service = service;
        }

        // GET: ediary/students
        [Route("")]
        [Authorize(Roles = "admins, teachers")]
        public IEnumerable<StudentDTO> GetStudents()
        {
            return service.GetStudents();
        }

        // GET: ediary/students/5
        [Route("{id}")]
        [ResponseType(typeof(Student))]
        [Authorize(Roles = "admins , teachers, students, parents")]
        public IHttpActionResult GetStudentById(string id)
        {
            StudentDTO studentDto = service.GetStudentById(id);
            if (studentDto == null)
            {
                return NotFound();
            }

            return Ok(studentDto);
        }

        // POST: ediary/students
        [AllowAnonymous]
        [Route("")]
        [Authorize(Roles = "admins")]
        public async Task<IHttpActionResult> PostStudent(StudentDTO studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Student postedStudent = service.PostStudent(studentDto);
            //return Created("", postedStudent);

            var result = await service.PostStudent(studentDto);

            return Ok();
        }

        // PUT: ediary/students/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutStudent(string id, StudentDTO studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutStudent(id, studentDto);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/students/5
        [Route("{id}")]
        [ResponseType(typeof(Student))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteStudent(string id)
        {
            Student student = service.DeleteStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: ediary/students/5/schoolclass/8-3-2018
        [ResponseType(typeof(Student))]
        [Route("{id}/schoolclass/{schoolClassId}", Name = "AddSchoolClass")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutSchoolClass(string id, string schoolClassId)
        {
            Student student = service.PutSchoolClass(id, schoolClassId);

            if (student == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddSchoolClass", new { id = student.Id, schoolClassId = student.SchoolClassId }, student);
        }

        // PUT: ediary/students/5/parent/5
        [ResponseType(typeof(Student))]
        [Route("{id}/parent/{parentId}", Name = "AddParent")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutParent(string id, string parentId)
        {
            Student student = service.PutParent(id, parentId);

            if (student == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddParent", new { id = student.Id, parentId = student.ParentId }, student);
        }

        // GET: ediary/students/by-username/petar.petrovic/get
        [Route("by-username/{username}/get")]            // ovo get na kraju sam dodao jer ne moze da primi url sa tackom na kraju, npr: "by-username/jovana.popovic". Ako nije tacka na kraju onda radi, pa stoga mora da se doda bilo sta na kraj npr: "by-username/jovana.popovic/get"
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetStudentByUsername(string username)
        {
            Student student = service.GetStudentByUsername(username);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // GET: ediary/students/by-firstOrLastName/Petar
        [Route("by-firstOrLastName/{pattern}")]
        [Authorize(Roles = "admins, teachers, parents")]
        public IEnumerable<StudentDTO> GetStudentsByName(string pattern)
        {
            IEnumerable<StudentDTO> studentsDto = service.GetStudentsByName(pattern);

            return studentsDto;
        }

    }
}
