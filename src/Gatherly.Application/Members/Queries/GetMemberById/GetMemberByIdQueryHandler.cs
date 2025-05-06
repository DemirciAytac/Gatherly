
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Common.Exceptions;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Queries.GetMemberById
{
    public sealed class GetMemberByIdQueryHandler : IQueryHandler<GetMemberByIdQuery, MemberResponse>
    {
        private readonly IMemberRepository _memberRepository;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Result<MemberResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(
                       request.MemberId,
                       cancellationToken);

            if (member is null)
            {
                return Result.Failure<MemberResponse>(new Error(
                    "Member.NotFound",
                    $"The member with Id {request.MemberId} was not found"));
            }

            var response = new MemberResponse(member.Id, 
                                              member.Email.Value, 
                                              member.Addresses.Select(x => new DTOs.AddressDTO(x.Street, x.City,x.ZipCode)).ToList());

            return Result.Success<MemberResponse>(response);
        }
    }
}
