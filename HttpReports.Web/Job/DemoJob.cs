using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.Job
{
    public class DemoJob:BaseJob
    {
        public override string cron { get => "0/3 * * * * ?"; }

        public override Task Execute(IJobExecutionContext context)
        { 
            Console.WriteLine("-------------- DemoJob -----------------");
            Console.WriteLine(DateTime.Now);

            return Task.CompletedTask;
        } 

    }
}
