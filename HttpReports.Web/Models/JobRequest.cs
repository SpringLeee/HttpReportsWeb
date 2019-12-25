using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.Models
{
    public class JobRequest
    { 
        public int Id { get; set; }

        public string Title { get; set; }

        public string CronLike { get; set; }

        public string Rate { get; set; }

        public string Email { get; set; }

        public int Status { get; set; }

        public string Node { get; set; }

        public int RtStatus { get; set; }

        public string RtTime { get; set; }

        public string RtRate { get; set; }

        public int HttpStatus { get; set; }

        public string HttpCodes { get; set; }

        public string HttpRate { get; set; }

        public int IPStatus { get; set; }

        public string IPWhiteList { get; set; }

        public string IPRate { get; set; }  

    }
}
