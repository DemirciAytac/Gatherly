using FluentValidation;
using Gatherly.Application.Common.Behaviors;
using Gatherly.Infrastructure.BackgroundJobs;
using MediatR;
using Quartz;
using System.Reflection;

namespace Gatherly.App
{
    public static class AssemblyReference
    {
        public static IServiceCollection AddApp(this IServiceCollection services)
        {
            AddQuartz(services);

            return services;
        }

        public static IServiceCollection AddQuartz(IServiceCollection services)
        {
            services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
                         .AddTrigger(
                             trigger =>
                                 trigger.ForJob(jobKey)
                                        .WithSimpleSchedule(
                                             schedule =>
                                                schedule.WithIntervalInSeconds(10)
                                                         .RepeatForever()));
                configure.UseMicrosoftDependencyInjectionJobFactory();

 
            });

            services.AddQuartzHostedService();

            return services;
        }
    }
}
