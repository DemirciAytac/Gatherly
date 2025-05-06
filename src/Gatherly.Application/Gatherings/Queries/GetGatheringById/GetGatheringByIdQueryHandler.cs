
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Attendees.Queries.GetAttendeById;
using Gatherly.Application.Common.Exceptions;
using Gatherly.Application.Invitations.Queries.GetInvitationById;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using System.Linq;

namespace Gatherly.Application.Gatherings.Queries.GetGatheringById
{
    public sealed class GetGatheringByIdQueryHandler : IQueryHandler<GetGatheringByIdQuery, GatheringResponse>
    {
        private readonly IGatheringRepository _gatheringRepository;

        public GetGatheringByIdQueryHandler(IGatheringRepository gatheringRepository)
        {
            _gatheringRepository = gatheringRepository;
        }

        public async Task<Result<GatheringResponse>> Handle(GetGatheringByIdQuery request, CancellationToken cancellationToken)
        {
            Gathering? gathering = await _gatheringRepository.GetByIdAsync(request.gatheringId, cancellationToken);
           
            
            if(gathering == null)
            {
                return Result.Failure<GatheringResponse>(DomainErrors.Gathering.NotFound(request.gatheringId));
            }

            var response = new GatheringResponse(
                                        gathering.Id,
                                        gathering.Name,
                                        gathering.Location,
                                        $"{gathering.Creator.FirstName.Value} {gathering.Creator.LastName.Value}",
                                        gathering.Attendees.Select(attendee => new AttendeeResponse(
                                            attendee.MemberId,
                                            attendee.CreatedOnUtc
                                            )).ToList(),
                                        gathering.Invitations.Select(invitation => new InvitationResponse(
                                            invitation.Id,
                                            invitation.Status
                                            )).ToList()
                                        );

            return Result.Success<GatheringResponse>(response );                        

        }
    }
}
