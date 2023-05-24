using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Url_RAP_checker.Models.Db;
using Url_RAP_checker.Models.User;
using Url_RAP_checker.Module.Url;

namespace Url_RAP_checker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly URLContext _context;

        public UserController(URLContext uRLContext)
        {
            _context = uRLContext;
        }
        // GET: api/User
        [HttpGet("Login")]
        public async Task<Token> GetLogin([FromQuery] Users user)
        {
            Login L = new Login(_context);
            Token _Token = await L.LoginUser(user);
            return _Token;
        }

       // POST: api/User
        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] Users User)
        {
                 
            if (UserExists(User.Email))
            {
                return Conflict();
            }
            else
            {
                _context.Users.Add(User);
                await _context.SaveChangesAsync();
                
           }
            return Ok() ;
        }

        private bool UserExists(string Email)
        {
            return _context.Users.Any(e => e.Email == Email);
        }

    }
}
