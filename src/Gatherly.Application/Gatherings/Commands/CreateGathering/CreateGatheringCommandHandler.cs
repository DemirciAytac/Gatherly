using Gatherly.Application.Common.Exceptions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Shared;

namespace Gatherly.Application.Gatherings.Commands.CreateGathering
{
    public sealed class CreateGatheringCommandHandler : ICommandHandler<CreateGatheringCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IGatheringRepository _gatheringRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGatheringCommandHandler(IMemberRepository memberRepository, IGatheringRepository gatheringRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _gatheringRepository = gatheringRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ApplicationArgumentNullException($"{nameof(request)} can not be null.");
            }

            var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

            if (member is null)
            {
                throw new NotFoundException(nameof(Gathering), request.MemberId);
            }

            var gathering = Gathering.Create(
                Guid.NewGuid(),
                member,
                request.Type,
                request.ScheduledAtUtc,
                request.Name,
                request.Location,
                request.MaximumNumberOfAttendees,
                request.InvitationsValidBeforeInHours);

            _gatheringRepository.Add(gathering);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
