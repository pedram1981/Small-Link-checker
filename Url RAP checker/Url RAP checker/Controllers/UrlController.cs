using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Url_RAP_checker.Models.Db;
using Url_RAP_checker.Models.Url;

namespace Url_RPA_checker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly URLContext _context;

        public UrlController(URLContext uRLContext)
        {
            _context = uRLContext;
        }
        // GET: api/Urls    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Url>>> GetUrl()
        {
            return await _context.Url.ToListAsync();
        }

        // GET: api/Urls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Url>> GetUrl(long id)
        {
            var url = await _context.Url.FindAsync(id);

            if (url == null)
            {
                return NotFound();
            }

            return url;
        }

        // PUT: api/Urls/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUrl(long id, Url url)
        {
            if (id != url.Id)
            {
                return BadRequest();
            }

            _context.Entry(url).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrlExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Urls
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Url>> PostUrl(Url url)
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

            return CreatedAtAction("GetUrl", new { id = url.Id }, url);
        }

        // DELETE: api/Urls/5
        [HttpDelete("Delete")]
        public async Task<ActionResult<Url>> DeleteUrl([FromBody] DeleteQueryObject req)
        {
            UrlCrud U = new UrlCrud(_context);
            bool Result = await U.Delete(req.Id, req.Url);
            if (Result == false)
            {
                return NotFound();
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