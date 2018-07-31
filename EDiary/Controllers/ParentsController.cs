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
    [RoutePrefix("ediary/parents")]
    public class ParentsController : ApiController
    {
        private IParentsService service;

        public ParentsController(IParentsService service)
        {
            this.service = service;
        }

        // GET: ediary/parents
        [Route("")]
        [Authorize(Roles = "admins")]
        public IEnumerable<ParentDTO> GetParents()
        {
            return service.GetParents();
        }

        // GET: ediary/parents/5
        [Route("{id}")]
        [ResponseType(typeof(Parent))]
        [Authorize(Roles = "admins, parents")]
        public IHttpActionResult GetParentById(string id)
        {
            ParentDTO parentDto = service.GetParentById(id);
            if (parentDto == null)
            {
                return NotFound();
            }

            return Ok(parentDto);
        }

        // POST: ediary/parents
        [Route("")]
        [Authorize(Roles = "admins")]
        public async Task<IHttpActionResult> PostParent(ParentDTO parentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Parent postedParent = service.PostParent(parentDto);
            //return Created("", postedParent);

            var result = await service.PostParent(parentDto);

            return Ok();
        }

        // PUT: ediary/parents/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult PutParent(string id, ParentDTO parentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutParent(id, parentDto);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/parents/5
        [Route("{id}")]
        [ResponseType(typeof(Parent))]
        [Authorize(Roles = "admins")]
        public IHttpActionResult DeleteParent(string id)
        {
            Parent parent = service.DeleteParent(id);
            if (parent == null)
            {
                return NotFound();
            }          

            return Ok(parent);
        }

        // PUT: ediary/parents/5/schoolclass/8-3-2018
        [ResponseType(typeof(Parent))]
        [Route("{id}/student/{studentId}", Name = "AddStudentToParent")]
        [Authorize(Roles = "admins")]
        [HttpPut]
        public IHttpActionResult PutStudent(string id, string studentId)
        {
            Parent parent = service.PutStudent(id, studentId);

            if (parent == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("AddStudentToParent", new { id = parent.Id, studentId = studentId }, parent);
        }

        // GET: ediary/parents/by-username/petar.petrovic/get
        [Route("by-username/{username}/get")]            // ovo get na kraju sam dodao jer ne moze da primi url sa tackom na kraju, npr: "by-username/jovana.popovic". Ako nije tacka na kraju onda radi, pa stoga mora da se doda bilo sta na kraj npr: "by-username/jovana.popovic/get"
        [Authorize(Roles = "admins")]
        public IHttpActionResult GetParentByUsername(string username)
        {
            Parent parent = service.GetParentByUsername(username);
            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }

        // GET: ediary/parents/by-firstOrLastName/Petar
        [Route("by-firstOrLastName/{pattern}")]
        [Authorize(Roles = "admins")]
        public IEnumerable<ParentDTO> GetParentsByName(string pattern)
        {
            IEnumerable<ParentDTO> parentsDto = service.GetParentsByName(pattern);

            return parentsDto;
        }

        // GET: ediary/parents/22/students
        [Route("{id}/students")]
        [Authorize(Roles = "admins")]
        public IEnumerable<Student> GetParentStudents(string id)
        {
            IEnumerable<Student> students = service.GetParentStudents(id);
            if (students == null)
            {
                return new List<Student>();
            }

            return students;
        }
    }
}
