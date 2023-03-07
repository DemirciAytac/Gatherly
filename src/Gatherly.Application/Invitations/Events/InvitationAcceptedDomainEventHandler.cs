﻿using Gatherly.Application.Absractions;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitations.Events
{
    public sealed class InvitationAcceptedDomainEventHandler : INotificationHandler<InvitationAcceptedDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IGatheringRepository _gatheringRepository;

        public InvitationAcceptedDomainEventHandler(IEmailService emailService, IGatheringRepository gatheringRepository)
        {
            _emailService = emailService;
            _gatheringRepository = gatheringRepository;
        }

        public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
        {
            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(notification.GatheringId, cancellationToken);

            if(gathering is null)
            {
                return;
            }

            await _emailService.SendInvitationAcceptedAsync(gathering);
        }
    }
}
