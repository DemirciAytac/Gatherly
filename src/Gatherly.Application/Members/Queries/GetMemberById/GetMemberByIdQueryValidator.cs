using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Queries.GetMemberById
{
    public class GetMemberByIdQueryValidator : AbstractValidator<GetMemberByIdQuery>
    {
        public GetMemberByIdQueryValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty().WithMessage($"MemberId değeri null geçilemez.");
        }
    }
}
