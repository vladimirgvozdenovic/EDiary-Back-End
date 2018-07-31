using EDiary.Models;
using EDiary.Models.DTOs;
using EDiary.Services;
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
    [RoutePrefix("ediary/teachers")]
    public class TeachersController : ApiController
    {
        private ITeachersService service;

        public TeachersController(ITeachersService service)
        {
            this.service = service;
        }

        // GET: ediary/teachers
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherDTO> GetTeachers()
        {
            return service.GetTeachers();
        }

        // GET: ediary/teachers/5
        [Route("{id}")]
        [ResponseType(typeof(Teacher))]
        [Authorize(Roles = "admins, teachers")]
        public IHttpActionResult GetTeacherById(string id)
        {
            TeacherDTO teacherDto = service.GetTeacherById(id);
            if (teacherDto == null)
            {
                return NotFound();
            }

            return Ok(teacherDto);
        }

        // POST: ediary/teachers
        [AllowAnonymous]
        [Route("")]
        [Authorize(Roles = "admins")]
        //[ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> PostTeacher(TeacherDTO teacherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Teacher postedTeacher = service.PostTeacher(teacherDto);
            //return Created("", postedTeacher);

            var result = await service.PostTeacher(teacherDto);           

            return Ok();
        }

        // PUT: ediary/teachers/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutTeacher(string id, TeacherDTO teacherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutTeacher(id, teacherDto);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/teachers/5
        [Route("{id}")]
        [ResponseType(typeof(Teacher))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteTeacher(string id)
        {
            Teacher teacher = service.DeleteTeacher(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // PUT: ediary/teachers/5/schoolclass/8-3
        [ResponseType(typeof(Teacher))]
        [Route("ediary/teachers/{id}/schoolclass/{schoolClassId}", Name = "AddHeadClass")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutHeadClass(string id, string schoolClassId)
        {
            Teacher teacher = service.PutHeadClass(id, schoolClassId);

            if (teacher == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddHeadClass", new { id = teacher.Id, schoolClassId = teacher.HeadClass.Id }, teacher);
        }

        // GET: ediary/teachers/by-username/petar.petrovic/get
        [Route("by-username/{username}/get")]           // ovo get na kraju sam dodao jer ne moze da primi url sa tackom na kraju, npr: "by-username/jovana.popovic". Ako nije tacka na kraju onda radi, pa stoga mora da se doda bilo sta na kraj npr: "by-username/jovana.popovic/get"
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetTeacherByUsername(string username)
        {
            Teacher teacher = service.GetTeacherByUsername(username);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // GET: ediary/teachers/by-firstOrLastName/Petar
        [Route("by-firstOrLastName/{pattern}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<TeacherDTO> GetTeachersByName(string pattern)
        {
            IEnumerable<TeacherDTO> teachersDto = service.GetTeachersByName(pattern);

            return teachersDto;
        }

        // GET: ediary/teachers/{id}/Subjects
        [Route("{id}/subjects")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Subject> GetTeacherSubjects(string id)
        {
            IEnumerable<Subject> subjects = service.GetTeacherSubjects(id);

            return subjects;
        }

        //// GET: ediary/teachers/resetPassword/1
        //[Route("resetPassword/{id:int}")]
        //public IHttpActionResult PutResetPassword(string id)
        //{
        //    bool done = service.PutResetPassword(id);
        //    if (done == false)
        //    {
        //        return NotFound();
        //    }

        //    return Ok();
        //}

        //// GET: ediary/teachers/changePassword/1/oldPass/Mile123/newPass/Mile321
        //[Route("changePassword/{id:int}/oldPass/{oldPass}/newPass/{newPass}")]
        //public IHttpActionResult PutChangePassword(string id, string oldPass, string newPass)
        //{
        //    bool done = service.PutChangePassword(id, oldPass, newPass);
        //    if (done == false)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok();
        //}
    }
}
