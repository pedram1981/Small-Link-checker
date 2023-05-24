using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Url_RAP_checker.Models.Db;
using Url_RAP_checker.Module.Url;

namespace Url_RAP_checker.Models.Url
{
    public class UrlModel
    {
        private readonly URLContext _context;
        public UrlModel(URLContext uRLContext)
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

        public async Task<bool> CheckAll()
        {
            int houre = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            //houre = houre > 12 ? houre - 12 : houre;
            string H = houre.ToString().Length == 1 ? "0" + houre.ToString() : houre.ToString();
            string M = minute.ToString().Length == 1 ? "0" + minute.ToString() : minute.ToString();
            string Time = H + ":" + M;
            var Urls=_context.Url.Where(e => e.IntervalCheck == Time).ToList();
            foreach (var item in Urls)
            {
                Check c = new Check();
                string result =await c.CheckUrl(item.UrlCheck);
                bool r=await Update(item.Id, result);
                if(!result.Contains("Successful:"))
                {
                    SendEmailNotification s = new SendEmailNotification();
                    bool h=await s.SendEmail(item.Email, "Url broken", "This Url is Broken or has problem in domain:" + item.UrlCheck+"("+result+")");
                }
            }
            return true;
        }

        public async Task<bool> Update(long Id, string ResultCheck)
        {
            var Row =await _context.Url.FirstOrDefaultAsync(e => e.Id == Id);
            if (Row != null)
            {
                Row.ResultCheck = ResultCheck;
                Row.LastTimeCheck = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> UpdateUrl(long Id, Url_RAP_checker.Models.Db.Url url)
        {
          
            var Row = await _context.Url.FirstOrDefaultAsync(e => e.Id == Id);
            if (Row != null)
            {
                Row.ResultCheck = url.ResultCheck;
                Row.Name = url.Name;
                Row.UrlCheck = url.UrlCheck;
                Row.IntervalCheck = url.IntervalCheck;
                Row.LastTimeCheck = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
        //var id = context.Customers.Where(c => c.Email == "email@example.com").Select(c => c.Id).FirstOrDefault();

    }
}
