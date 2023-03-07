using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Gatherings.Commands.CreateGathering
{
    public class CreateGatheringCommandValidator : AbstractValidator<CreateGatheringCommand>
    {
        public CreateGatheringCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name required");
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}
