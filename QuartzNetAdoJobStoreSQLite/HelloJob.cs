using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzNetAdoJobStoreSQLite
{
    using Common.Logging;
    using Quartz;

    public class HelloJob : IJob
    {
        public virtual void Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey jobKey = context.JobDetail.Key;

                string message = string.Format("Job Key:{0} - Trigger Key:{1} - Start Time:{2} - Schedule Fire Time: {3}", context.JobDetail.Key, context.Trigger.Key, context.Trigger.StartTimeUtc, context.ScheduledFireTimeUtc);

                ILog log = LogManager.GetCurrentClassLogger();
                log.Debug(message);

                Console.WriteLine(new String('*', 100));

                Console.WriteLine("Trigger Info: " + message);
                Console.WriteLine("Next Schedule: " + context.Trigger.GetNextFireTimeUtc());

                Console.WriteLine(new String('*', 100));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                JobExecutionException e2 = new JobExecutionException(ex);
                e2.RefireImmediately = true;                                
                throw e2;
            }

        }
    }
}
