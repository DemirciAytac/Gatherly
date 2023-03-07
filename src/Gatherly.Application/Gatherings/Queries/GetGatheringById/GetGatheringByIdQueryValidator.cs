using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Gatherings.Queries.GetGatheringById
{
    public sealed class GetGatheringByIdQueryValidator : AbstractValidator<GetGatheringByIdQuery>
    {
        public GetGatheringByIdQueryValidator()
        {
            RuleFor(x => x.gatheringId).NotEmpty().WithMessage("GatheringId required.");
        }
    }
}
