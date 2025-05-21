using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Gatherings.Commands.CreateGathering
{
    public sealed record CreateGatheringCommand(
        Guid MemberId,
        GatheringType Type,
        DateTime ScheduledAtUtc,
        string Name,
        string? Location,
        int? MaximumNumberOfAttendees,
        int? InvitationsValidBeforeInHours) : ICommand;

}
