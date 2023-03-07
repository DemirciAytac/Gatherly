using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitations.Commands.SendInvitation
{
    public class SendInvitationCommandValidator : AbstractValidator<SendInvitationCommand>
    {
        public SendInvitationCommandValidator()
        {
            RuleFor(x => x.MemberId).NotNull().WithMessage("MemberId required.");
            RuleFor(x => x.GatheringId).NotNull().WithMessage("GatheringId required.");
        }
    }
}
