using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzNetAdoJobStoreSQLite
{
    using Quartz;
    using Quartz.Impl;
    using Quartz.Simpl;
    using Quartz.Spi;
    using Quartz.Xml;
    using Quartz.Xml.JobSchedulingData20;
    using System.Data.SQLite;
    using System.IO;
    using System.Xml.Serialization;

    class Program
    {
        public static StdSchedulerFactory SchedulerFactory;
        public static IScheduler Scheduler;

        static void Main(string[] args)
        {
            CreateQuartzNetTables();

            Console.WriteLine("sqlite tables create. press button to continue.");
            Console.ReadKey();

            SchedulerFactory = new StdSchedulerFactory();
            Scheduler = SchedulerFactory.GetScheduler();

            try
            {
                Scheduler.Start();
            }
            catch (Quartz.SchedulerConfigException ex)
            {
                RemoveNonExistingJobs(ex, Scheduler);
                Scheduler.Start();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            Console.WriteLine("Quartz.Net Started ...");

            Test001(Scheduler);

            Console.WriteLine("Quartz.Net Running ...");

            Console.ReadLine();

        }

        private static void Test001(IScheduler Scheduler)
        {
            IJobDetail helloJob = JobBuilder.Create<HelloJob>()
                                        .WithIdentity("HelloJob")
                                        // .RequestRecovery(true)
                                        // .StoreDurably(false)
                                        .Build();

            ITrigger helloTrigger = TriggerBuilder.Create()
                                        .WithIdentity("HelloTrigger")
                                        .StartNow()
                                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever())
                                        .Build();

            Scheduler.ScheduleJob(helloJob, helloTrigger);

        }

        private static bool RemoveNonExistingJobs(Exception ex, IScheduler scheduler)
        {
            IList<string> typesToRemove = new List<string>();

            Exception innerException = ex;

            while (innerException != null)
            {
                if (innerException is System.TypeLoadException)
                {
                    typesToRemove.Add((innerException as System.TypeLoadException).TypeName);
                }
                innerException = ParseExceptions(innerException);
            }

            if (typesToRemove.Count > 0)
            {
                foreach (var jobName in typesToRemove)
                {
                    if (!scheduler.CheckExists(new JobKey(jobName)))
                    {
                        scheduler.DeleteJob(new JobKey(jobName));
                    }
                }
            }

            return (true);
        }

        private static Exception ParseExceptions(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return (ex.InnerException);
            }
            return (null);
        }

        private static void CreateQuartzNetTables()
        {
            using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=data\quartznet.db;Version=3;Foreign Keys=ON;"))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = File.ReadAllText("tables_sqlite.sql");
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
