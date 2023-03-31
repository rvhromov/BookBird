using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Serilog;

namespace BookBird.Infrastructure.PipelineBehaviors
{
    public sealed class JobLoggingBehavior : IJobListener
    {
        public string Name => "LoggingListener";

        private readonly ILogger _logger;

        public JobLoggingBehavior(ILogger logger) =>
            _logger = logger;

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new())
        {
            var jobKey = context.JobDetail.Key.Name;

            _logger.Information("Starting job {@JobKey}, {@DateTimeUtc}",
                jobKey,
                DateTime.UtcNow);

            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new())
        {
            var jobKey = context.JobDetail.Key.Name;

            _logger.Information("Job {@JobKey} vetoed, {@DateTimeUtc}",
                jobKey,
                DateTime.UtcNow);

            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException,
            CancellationToken cancellationToken = new())
        {
            var jobKey = context.JobDetail.Key.Name;

            if (jobException is not null)
            {
                _logger.Information("Job failure {@JobKey}, {@ExceptionMessage} {@DateTimeUtc}",
                    jobKey,
                    jobException.GetBaseException().Message,
                    DateTime.UtcNow);
            }

            _logger.Information("Completed job {@JobKey}, {@DateTimeUtc}",
                jobKey,
                DateTime.UtcNow);

            return Task.CompletedTask;
        }
    }
}