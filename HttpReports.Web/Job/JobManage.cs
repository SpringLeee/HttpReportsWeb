using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.Job
{
    public  class JobManage
    { 
        public ScheduleHelper Start()
        { 
            var Job = new ScheduleHelper();

            Job.Excute<TestJob>();  
          
            Job.Start();

            return Job;
        } 
    }
}
