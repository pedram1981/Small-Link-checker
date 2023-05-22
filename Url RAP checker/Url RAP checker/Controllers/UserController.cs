using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Url_RAP_checker.Module.Url;

namespace Url_RAP_checker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User
        [HttpGet]
        public async Task<string> Get()
        {
            Check c = new Check();
            await c.MyAction("http://www.googlesdsa.com");
            return "hello";
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<string> Get(int id)
        {
            SendEmailNotification send = new SendEmailNotification();
            string body1 = "<h3>Email verification Code</h3>" +
    "<span style=\"color:#902cfe;\">URL BROKEN</span>" +
    "<p style=\"color:#000000;\">Please check this Url,may be broken or some thing like this</p>";
            string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
            body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
            body += "</HEAD><BODY><DIV>";
            body += body1;
            body += "</DIV></BODY></HTML>";
            await send.SendEmail("pedram.azar60@gmail.com", "Url broken", body);
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
