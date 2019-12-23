using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;

namespace HttpReports.Web.Job
{
    public class TestJob:BaseJob
    {
        public override string cron { get => "0/3 * * * * ?"; }

        public override Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now);

            return Task.CompletedTask; 
        }  
    }
}
