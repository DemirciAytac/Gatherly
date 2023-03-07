using Gatherly.Application.Absractions;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Events
{
    public sealed class SendWelcomeEmailWhenMemberRegisteredDomainEventHandler : INotificationHandler<MemberRegisteredDomainEvent>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IEmailService _emailService;

        public SendWelcomeEmailWhenMemberRegisteredDomainEventHandler(IMemberRepository memberRepository, IEmailService emailService)
        {
            _memberRepository = memberRepository;
            _emailService = emailService;
        }

        public async Task Handle(MemberRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetByIdAsync(notification.MemberId, cancellationToken);
            
            if(member is null)
            {
                return;
            }

            await _emailService.SendWelcomeEmailAsync(member, cancellationToken);

        }
    }
}
