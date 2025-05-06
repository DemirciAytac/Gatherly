using FluentValidation;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Commands.CreateMember
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        private readonly IMemberRepository _memberRepository;

        public CreateMemberCommandValidator(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                                 .WithMessage("Email required")
                                 .MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(FirstName.MaxLength);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(LastName.MaxLength);
        }
        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
              return await _memberRepository.IsEmailUniqueAsync(Email.Create(email).Value);
        }
    }
}
