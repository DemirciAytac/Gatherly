using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation
{
    public class AcceptInvitationCommandValidator: AbstractValidator<AcceptInvitationCommand>
    {
        public AcceptInvitationCommandValidator()
        {
            RuleFor(x => x.gatheringId).NotNull().WithMessage("GatheringId required.");
            RuleFor(x => x.InvitationId).NotNull().WithMessage("InvitationId required.");
        }
    }
}
