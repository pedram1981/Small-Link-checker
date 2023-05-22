using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Url_RAP_checker.Models.Db;

namespace Url_RAP_checker.Models.Url
{
    public class UrlCrud
    {
        private readonly URLContext _context;
        public UrlCrud(URLContext uRLContext)
        {
            _context = uRLContext;
        }
        public async Task<bool> Delete(int id,string Url)
        {
            try
            {
                 var url = await _context.Url.FirstOrDefaultAsync(e => e.Id == id && e.UrlCheck == Url);
                if (url == null)
                {
                    return false;
                }

                _context.Url.Remove(url);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string hh = e.Message;
                throw;
            }
            
            

            return true;
        }

    }
}
