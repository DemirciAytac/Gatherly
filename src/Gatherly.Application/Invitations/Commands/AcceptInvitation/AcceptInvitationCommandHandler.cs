using Gatherly.Application.Absractions;
using Gatherly.Application.Common.Exceptions;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation
{
    internal sealed class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand>
    {
        private readonly IInvitationRepository _invatationRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmailService _emailService;

        public AcceptInvitationCommandHandler(
            IInvitationRepository invatationRepository, 
            IMemberRepository memberRepository,
            IGatheringRepository gatheringRepository, 
            IAttendeeRepository attendeeRepository, 
            IUnitOfWork unitOfWork
            //IEmailService emailService
            )
        {
            _invatationRepository = invatationRepository;
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _attendeeRepository = attendeeRepository;
            _unitOfWork = unitOfWork;
           // _emailService = emailService;
        }

        public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationArgumentNullException($"{nameof(request)} can not be null.");
            }

            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.gatheringId, cancellationToken);
            
            if(gathering is null)
            {
                return Unit.Value;
            }

            var invitation = gathering.Invitations.FirstOrDefault(x => x.Id == request.InvitationId);

            if (invitation is null || invitation.Status != InvitationStatus.Pending)
            {
                return Unit.Value;
            }

            var attendee = gathering.AcceptInvitation(invitation);

            if(attendee is not null)
            {
                _attendeeRepository.Add(attendee);
            }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
