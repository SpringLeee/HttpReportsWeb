using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpReports.Web.Job
{
    public class ScheduleHelper
    {
        // 调度器
        private IScheduler scheduler = null;

        public ScheduleHelper()
        {
            //初始化调度器

            if (scheduler == null)
            {
                scheduler = new StdSchedulerFactory().GetScheduler().Result;
            }
        }

        public void Excute<T>() where T : BaseJob, new()
        {
            var cron = new T().cron;

            if (CronExpression.IsValidExpression(cron))
            {
                var job = JobBuilder.Create<T>().Build();

                var trigger = TriggerBuilder.Create().WithCronSchedule(cron).Build();

                scheduler.ScheduleJob(job, trigger);

            }
        }

        public void Start()
        {
            scheduler.Start();
        }
    }
}
