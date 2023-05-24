using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Url_RAP_checker.Models.Db;

namespace Url_RAP_checker.Models.User
{
    public class UsersModels
    {
        private readonly URLContext _context;
        public UsersModels(URLContext uRLContext)
        {
            _context = uRLContext;
        }


    }
}
