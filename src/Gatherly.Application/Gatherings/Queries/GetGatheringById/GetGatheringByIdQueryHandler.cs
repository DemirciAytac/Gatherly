

using Gatherly.Application.Absractions.Messaging;
using Gatherly.Application.Attendees.Queries.GetAttendeById;
using Gatherly.Application.Common.Exceptions;
using Gatherly.Application.Invitations.Queries.GetInvitationById;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
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

        public async Task<GatheringResponse> Handle(GetGatheringByIdQuery request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationArgumentNullException($"{nameof(request)} can not be null.");
            }

            Gathering? gathering = await _gatheringRepository.GetByIdAsync(request.gatheringId, cancellationToken);
           
            
            if(gathering == null)
            {
                throw new NotFoundException(nameof(Gathering), request.gatheringId);
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

            return response;                         

        }
    }
}
