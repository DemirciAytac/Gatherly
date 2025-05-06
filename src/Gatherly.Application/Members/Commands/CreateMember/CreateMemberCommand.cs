

using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.DTOs;

namespace Gatherly.Application.Members.Commands.CreateMember
{
    public sealed record CreateMemberCommand(
        string Email,
        string FirstName,
        string LastName,
        List<AddressDTO> Addresses
        ) : ICommand<Guid>;

}
