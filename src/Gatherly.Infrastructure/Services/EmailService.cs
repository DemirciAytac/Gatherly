using Gatherly.Application.Absractions;
using Gatherly.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Infrastructure.Services
{
    public sealed class EmailService : IEmailService
    {
        public Task SendInvitationAcceptedAsync(Gathering gathering)
        {
            throw new NotImplementedException();
        }

        public Task SendInvitationSentEmailAsync(Member member, Gathering gathering)
        {
            throw new NotImplementedException();
        }

        public async Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken = default)
        {
            // Unit.Value;
            //throw new NotImplementedException();
        }
    }
}
