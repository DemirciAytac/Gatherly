using Gatherly.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Queries.GetMemberById
{
    public sealed record MemberResponse(Guid id, string email, List<AddressDTO> addresses);
}
