﻿using Gatherly.Application.Abstractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation
{
    public sealed record AcceptInvitationCommand(Guid gatheringId, Guid InvitationId) : ICommand;
}
