using Gatherly.Domain.Repositories;
using Gatherly.Persistence.Interceptors;
using Gatherly.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Persistence
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(Assembly).Assembly;
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IGatheringRepository, GatheringRepository>();
            services.AddScoped<IAttendeeRepository, AttendeRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<ConvertDomainEventToOutboxMessageInterceptor>();
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, optionBuilder) =>
            {
                var outboxInterceptor = sp.GetService<ConvertDomainEventToOutboxMessageInterceptor>();
                var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>();
                var connectionString = configuration.GetConnectionString("Default");
                var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
                connectionString = string.Format(connectionString, password);
                DbContextOptionsBuilder dbContextOptionsBuilder = optionBuilder.UseSqlServer(
                    connectionString
                    // ,o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    ).AddInterceptors(outboxInterceptor, auditableInterceptor);
            });
            return services;
        }
    }
}
