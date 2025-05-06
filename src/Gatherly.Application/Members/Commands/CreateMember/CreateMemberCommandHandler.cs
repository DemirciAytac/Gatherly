

using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Errors;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Gatherly.Domain.ValueObjects;
using MediatR;


namespace Gatherly.Application.Members.Commands.CreateMember
{
    public sealed class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, Guid>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {

            Result<Email> emailResult = Email.Create(request.Email);
            Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
            Result<LastName> lastName = LastName.Create(request.LastName);

            if (!await _memberRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken))
            {
                return Result.Failure<Guid>(DomainErrors.Gathering.AlreadyPassed);
            }

            // Address oluşturma ve doğrulama
            var addressResults = request.Addresses
                .Select(a => Address.Create(a.city, a.street, a.zipCode))
                .ToList();

            var member = Member.Create(Guid.NewGuid(), emailResult.Value, firstNameResult.Value, lastName.Value);

            // Address'leri member'a ekle
            foreach (var addressResult in addressResults)
            {
                member.AddAddress(addressResult.Value);
            }
            _memberRepository.Add(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(member.Id);
        }

    }
}
