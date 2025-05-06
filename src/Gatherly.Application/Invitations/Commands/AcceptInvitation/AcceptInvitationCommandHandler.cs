using Gatherly.Application.Absractions;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Common.Exceptions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation
{
    internal sealed class AcceptInvitationCommandHandler : ICommandHandler<AcceptInvitationCommand>
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

        public async Task<Result> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationArgumentNullException($"{nameof(request)} can not be null.");
            }

            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.gatheringId, cancellationToken);
            
            if(gathering is null)
            {
                return  Result.Failure(DomainErrors.Gathering.NotFound(request.gatheringId));
            }

            var invitation = gathering.Invitations.FirstOrDefault(x => x.Id == request.InvitationId);

            if (invitation is null || invitation.Status != InvitationStatus.Pending)
            {
                return Result.Failure(DomainErrors.Invitation.AlreadyAccepted(request.InvitationId));
            }

            Result<Attendee> attendeeResult = gathering.AcceptInvitation(invitation);

            if (attendeeResult.IsSuccess)
            {
                _attendeeRepository.Add(attendeeResult.Value);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

    }
}
