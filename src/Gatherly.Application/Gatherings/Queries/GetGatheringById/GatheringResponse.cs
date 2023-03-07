

using Gatherly.Application.Attendees.Queries.GetAttendeById;
using Gatherly.Application.Invitations.Queries.GetInvitationById;
using Gatherly.Domain.Entities;

namespace Gatherly.Application.Gatherings.Queries.GetGatheringById
{
    public sealed record GatheringResponse(Guid Id, 
                                           string Name, 
                                           string Location, 
                                           string Creator,
                                           List<AttendeeResponse> Attendees,
                                           List<InvitationResponse> Invitations);
}