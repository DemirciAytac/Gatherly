using Gatherly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Absractions
{
    public interface IEmailService
    {
        Task SendInvitationSentEmailAsync(Member member, Gathering gathering);
        Task SendInvitationAcceptedAsync(Gathering gathering);
        Task SendWelcomeEmailAsync(Member member, CancellationToken cancellationToken = default);
    }
}
