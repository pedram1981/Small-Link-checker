using System;
using System.Collections.Generic;

namespace Url_RAP_checker.Models.Db
{
    public partial class Url
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string UrlCheck { get; set; }
        public string ResultCheck { get; set; }
        public string StatuseCheck { get; set; }
        public string IntervalCheck { get; set; }
        public DateTime? LastTimeCheck { get; set; }
    }
}
