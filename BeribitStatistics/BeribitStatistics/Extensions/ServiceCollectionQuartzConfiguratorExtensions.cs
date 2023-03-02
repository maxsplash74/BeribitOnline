using System;
using Microsoft.Extensions.Configuration;
using Quartz;

namespace BeribitStatistics.Extensions
{
    public static class ServiceCollectionQuartzConfiguratorExtensions
    {
        public static void AddJobAndTrigger<T>(
            this IServiceCollectionQuartzConfigurator quartz,
            IConfiguration config)
            where T : IJob
        {
            var jobName = typeof(T).Name;

            var configKey = $"Quartz:{jobName}";
            var cronSchedule = config[configKey];

            if (string.IsNullOrEmpty(cronSchedule))
                throw new Exception($"Не найдена конфигурация Quartz {configKey}");

            var jobKey = new JobKey(jobName);
            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(jobName + "-trigger")
                .WithCronSchedule(cronSchedule));
        }
    }
}
