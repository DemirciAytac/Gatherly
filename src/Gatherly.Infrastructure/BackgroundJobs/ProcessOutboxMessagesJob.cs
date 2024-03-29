﻿using Gatherly.Domain.Primitives;
using Gatherly.Persistence;
using Gatherly.Persistence.Outbox;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
          TypeNameHandling = TypeNameHandling.All
        };

        private readonly ApplicationDbContext _dbContext;
        private readonly IPublisher _publisher;

        public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<OutboxMessage> messages = await _dbContext.Set<OutboxMessage>()
                                                            .Where(x => x.ProcessedOnUtc == null)
                                                            .Take(20)
                                                            .ToListAsync(context.CancellationToken);

            foreach (OutboxMessage outboxMessage in messages)
            {
                IDomainEvent? domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(outboxMessage.Content,JsonSerializerSettings); ;

                if (domainEvent is null)
                {
                    continue;
                }

                AsyncRetryPolicy policy = Policy
                       .Handle<Exception>()
                       .WaitAndRetryAsync(3,
                                          attempt => TimeSpan.FromMilliseconds(attempt * 50));

                PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>
                     _publisher.Publish(domainEvent, context.CancellationToken)
                     );

                outboxMessage.Error = result.FinalException?.ToString();
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
