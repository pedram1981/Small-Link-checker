using System;
using System.Collections.Generic;

namespace Url_RAP_checker.Models.Db
{
    public partial class Url
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UrlCheck { get; set; }
        public string ResultCheck { get; set; }
        public string IntervalCheck { get; set; }
        public DateTime? LastTimeCheck { get; set; }
    }
}
