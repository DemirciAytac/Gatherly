using Gatherly.Application.Absractions;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Common.Exceptions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitations.Commands.SendInvitation
{
    internal sealed class SendInvitationCommandHandler : ICommandHandler<SendInvitationCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IInvitationRepository _invatationRepository;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public SendInvitationCommandHandler(
            IMemberRepository memberRepository, 
            IGatheringRepository gatheringRepository, 
            IInvitationRepository invatationRepository, 
            IEmailService emailService, 
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _invatationRepository = invatationRepository;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationArgumentNullException($"{nameof(request)} can not be null.");
            }

            var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

            if (member is null || gathering is null)
            {
                return Result.Failure(DomainErrors.Gathering.NotFound(request.GatheringId));
            }


            Result<Invitation> invitationResult = gathering.SendInvitation(member);

            if (invitationResult.IsFailure)
            {
                // Log error
                return invitationResult;
            }

            _invatationRepository.Add(invitationResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _emailService.SendInvitationSentEmailAsync(member, gathering);

            return Result.Success();
        }
    }
}
