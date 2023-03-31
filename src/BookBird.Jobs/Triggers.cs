using BookBird.Infrastructure.PipelineBehaviors;
using BookBird.Jobs.Jobs;
using BookBird.Jobs.Options;
using Quartz;

namespace BookBird.Jobs
{
    internal static class Triggers
    {
        public static IServiceCollectionQuartzConfigurator AddBookIndexingTrigger(
            this IServiceCollectionQuartzConfigurator configurator, CronSchedulersOptions cronSchedulers)
        {
            configurator
                .AddJob<BookIndexingJob>(opts => opts.WithIdentity(BookIndexingJob.Key))
                .AddTrigger(opts => opts
                .ForJob(BookIndexingJob.Key)
                .WithCronSchedule(cronSchedulers.BookIndexingJob))
                .AddJobListener<JobLoggingBehavior>();
            
            return configurator;
        }
    }
}