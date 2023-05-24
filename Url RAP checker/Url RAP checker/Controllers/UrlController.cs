using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Url_RAP_checker.Models.Db;
using Url_RAP_checker.Models.Url;
using Url_RAP_checker.Module.Url;

namespace Url_RPA_checker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UrlController : ControllerBase
    {
        private readonly URLContext _context;

        public UrlController(URLContext uRLContext)
        {
            _context = uRLContext;
        }
          
        //Show all url based on user's email adrres
        [HttpGet("ShowAll")]
        public async Task<ActionResult<IEnumerable<Url>>> GetUrl([FromQuery] string Email)
        {
            var url =await _context.Url.Where(e => e.Email == Email)
                .OrderByDescending(e => e.Id).ToListAsync();
            return url;
        }

        //Update data
        [HttpPut("update")]
        public async Task<IActionResult> PutUrl([FromQuery] string Email,[FromQuery]string UrlCheck, Url url)
        {
            var Id = await _context.Url.Where(e => e.Email == Email && e.UrlCheck == UrlCheck)
               .Select(e => e.Id).FirstOrDefaultAsync();
            url.LastTimeCheck = DateTime.Now;
            Check c = new Check();
            string result = await c.CheckUrl(url.UrlCheck);
            url.ResultCheck = result;
            if (0 < Id)
            {
                UrlModel u = new UrlModel(_context);
                url.ResultCheck = result;
                bool r = await u.UpdateUrl(Id, url);
                JsonResult j = new JsonResult(result);
                return j;
            }
            else
            {
                return NoContent();
            }
           
        }

    //check and save
        [HttpPost("check")]
        public async Task<ActionResult<Url>> PostUrl(Url url)
        {
            var Id =await _context.Url.Where(e => e.Email == url.Email && e.UrlCheck == url.UrlCheck)
                .Select(e => e.Id).FirstOrDefaultAsync();
            url.LastTimeCheck= DateTime.Now;
            Check c = new Check();
            string result = await c.CheckUrl(url.UrlCheck);
            url.ResultCheck = result;
            if (0<Id)
            {
                UrlModel u = new UrlModel(_context);
                bool r = await u.Update(Id, result);
             }
            else
            {
                _context.Url.Add(url);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (UrlExists(url.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

               
            }
            JsonResult j = new JsonResult(result);
            return j;
        }

        // DELETE
        [HttpDelete("Delete")]
        public async Task<ActionResult<Url>> DeleteUrl([FromBody] DeleteQueryObject req)
        {
            UrlModel U = new UrlModel(_context);
            bool Result = await U.Delete(req.Id, req.Url);
            if (Result == false)
            {
                JsonResult json_ = new JsonResult("These id and url are not exist");
                return json_;
            }

            return Ok();
        }
        public class DeleteQueryObject
        {
            public int Id { get; set; }
            public string Url { get; set; }
        }

        private bool UrlExists(long id)
        {
            return _context.Url.Any(e => e.Id == id);
        }
    }
}