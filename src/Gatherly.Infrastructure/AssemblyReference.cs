using Gatherly.Application.Absractions;
using Gatherly.Infrastructure.Idempotence;
using Gatherly.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gatherly.Infrastructure
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(Assembly).Assembly;
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
            return services;
        }
        }
}