using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Url_RAP_checker.Module.Url
{
    public class Check
    {
        public async Task<string> MyAction(string Link)
        {
            try
            {
                using var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(Link);
                if (response.IsSuccessStatusCode)
                    return "ok";
                else
                    return response.StatusCode.ToString();
            }
            catch (Exception e)
            {
                string ss = e.Message;
                return "sss";
            }
            
        }
    }
}
