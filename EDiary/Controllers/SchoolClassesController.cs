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
    [RoutePrefix("ediary/schoolClasses")]
    public class SchoolClassesController : ApiController
    {
        private ISchoolClassesService service;

        public SchoolClassesController(ISchoolClassesService service)
        {
            this.service = service;
        }

        // GET: ediary/schoolClasses
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<SchoolClass> GetSchoolClasses()
        {
            return service.GetSchoolClasses();
        }

        // GET: ediary/schoolClasses/5
        [Route("{id}")]
        [ResponseType(typeof(SchoolClass))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetSchoolClassById(string id)
        {
            SchoolClass schoolClass = service.GetSchoolClassById(id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return Ok(schoolClass);
        }

        // POST: ediary/schoolClasses
        [Route("")]
        [ResponseType(typeof(SchoolClass))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PostSchoolClass(SchoolClassDTO schoolClassDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SchoolClass postedSchoolClass = service.PostSchoolClass(schoolClassDto);

            if (postedSchoolClass == null)
            {
                return BadRequest();
            }

            return Created("", postedSchoolClass);
        }

        // PUT: ediary/schoolClasses/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutSchoolClass(string id, SchoolClassDTO schoolClassDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutSchoolClass(id, schoolClassDto);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/schoolClasses/8-3/2018
        [Route("{id}")]
        [ResponseType(typeof(SchoolClass))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteSchoolClass(string id)
        {
            SchoolClass schoolClass = service.DeleteSchoolClass(id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return Ok(schoolClass);
        }

        // PUT: ediary/schoolClasses/5/headTeacher/1
        [ResponseType(typeof(SchoolClass))]
        [Route("{id}/headTeacher/{headTeacherId}", Name = "AddHeadTeacher")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutHeadTeacher(string id, string headTeacherId)
        {
            SchoolClass schoolClass = service.PutHeadTeacher(id, headTeacherId);

            if (schoolClass == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddHeadTeacher", new { id = schoolClass.Id, headTeacherId = schoolClass.HeadTeacher.Id }, schoolClass);
        }

        // PUT: ediary/schoolClasses/5/student/5
        [ResponseType(typeof(Student))]
        [Route("{id}/student/{studentId}", Name = "AddStudent")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutStudent(string id, string studentId)
        {
            SchoolClass schoolClass = service.PutStudent(id, studentId);

            if (schoolClass == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddStudent", new { id = schoolClass.Id, studentId = studentId }, schoolClass);
        }

        // PUT: ediary/schoolClasses/5/teacherTeachSubjectToSchoolClass/5
        [ResponseType(typeof(Student))]
        [Route("{id}/teacherTeachSubjectToSchoolClass/{teacherTeachSubjectToSchoolClassId}", Name = "AddTeacherTeachSubjectToSchoolClass")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutTeacherTeachSubjectToSchoolClass(string id, int teacherTeachSubjectToSchoolClassId)
        {
            SchoolClass schoolClass = service.PutTeacherTeachSubjectToSchoolClass(id, teacherTeachSubjectToSchoolClassId);

            if (schoolClass == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddTeacherTeachSubjectToSchoolClass", new { id = schoolClass.Id, teacherTeachSubjectToSchoolClassId = teacherTeachSubjectToSchoolClassId }, schoolClass);
        }

        // GET: ediary/schoolClasses/by-calendarYear/2018
        [Route("by-calendarYear/{calendarYear:int}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<SchoolClass> GetSchoolClassesByCalendarYear(int calendarYear)
        {
            return service.GetSchoolClassesByCalendarYear(calendarYear);
        }

        // GET: ediary/schoolClasses/by-calendarYearAndSchoolYear/2018/8
        [Route("by-calendarYearAndSchoolYear/{calendarYear:int}/{schoolYear}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<SchoolClass> GetSchoolClassesByCalendarYearAndSchoolYear(int calendarYear, int schoolYear)
        {
            return service.GetSchoolClassesByCalendarYearAndSchoolYear(calendarYear, (SchoolYearEnum)schoolYear);
        }

        // GET: ediary/schoolClasses/8-3-2018/students
        [Route("{id}/students")]
        [Authorize(Roles = "admins")]
        public IEnumerable<StudentDTO> GetSchoolClassStudents(string id)
        {
            IEnumerable <StudentDTO> studentsDto = service.GetSchoolClassStudents(id);
            if (studentsDto == null)
            {
                return null;
            }

            return studentsDto;
        }

        // GET: ediary/schoolClasses/8-3-2018/teachers
        [Route("{id}/teachers")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherDTO> GetSchoolClassTeachers(string id)
        {
            IEnumerable<TeacherDTO> teachersDto = service.GetSchoolClassTeachers(id);
            if (teachersDto == null)
            {
                return null;
            }

            return teachersDto;
        }

        // GET: ediary/schoolClasses/8-3-2018/subjects
        [Route("{id}/subjects")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Subject> GetSchoolClassSubjects(string id)
        {
            IEnumerable<Subject> subjects = service.GetSchoolClassSubjects(id);
            if (subjects == null)
            {
                return null;
            }

            return subjects;
        }
    }
}
