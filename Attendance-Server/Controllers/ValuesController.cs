using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceServer.Entities;
using AttendanceServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_Server.Controllers
{
    [Authorize]
    [Route("attendance/api/login")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IAdminService _adminService;

        public ValuesController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<Admin> Post([FromBody] Admin admin)
        {
            var user = _adminService.Authenticate(admin.Username, admin.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
