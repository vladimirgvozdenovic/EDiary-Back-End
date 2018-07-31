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
    [RoutePrefix("ediary/admins")]
    [Authorize(Roles = "admins")]
    public class AdminUsersController : ApiController
    {
        private IAdminUsersService service;

        public AdminUsersController(IAdminUsersService service)
        {
            this.service = service;
        }

        // GET: ediary/admins
        [Route("")]
        public IEnumerable<AdminUserDTO> GetAdminUsers()
        {
            return service.GetAdminUsers();
        }

        // GET: ediary/admins/5
        [Route("{id}")]
        [ResponseType(typeof(AdminUser))]
        public IHttpActionResult GetAdminUserById(string id)
        {
            AdminUserDTO adminDto = service.GetAdminUserById(id);
            if (adminDto == null)
            {
                return NotFound();
            }

            return Ok(adminDto);
        }

        // POST: ediary/admins
        [Route("")]
        public async Task<IHttpActionResult> PostAdminUser(AdminUserDTO adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Parent postedParent = service.PostParent(parentDto);
            //return Created("", postedParent);

            var result = await service.PostAdminUser(adminDto);

            return Ok();
        }

        // PUT: ediary/admins/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdminUser(string id, AdminUserDTO adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool done = service.PutAdminUser(id, adminDto);
            if (done == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: ediary/admins/5
        [Route("{id}")]
        [ResponseType(typeof(AdminUser))]
        public IHttpActionResult DeleteAdminUser(string id)
        {
            AdminUser admin = service.DeleteAdminUser(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // GET: ediary/admins/by-username//petar.petrovic/get
        [Route("by-username/{username}/get")]            // ovo get na kraju sam dodao jer ne moze da primi url sa tackom na kraju, npr: "by-username/jovana.popovic". Ako nije tacka na kraju onda radi, pa stoga mora da se doda bilo sta na kraj npr: "by-username/jovana.popovic/get"
        public IHttpActionResult GetAdminUserByUsername(string username)
        {
            AdminUser admin = service.GetAdminUserByUsername(username);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }
    }
}
