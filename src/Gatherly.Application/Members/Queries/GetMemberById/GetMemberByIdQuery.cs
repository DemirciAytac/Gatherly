using Gatherly.Application.Absractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Members.Queries.GetMemberById
{
    public sealed record GetMemberByIdQuery(Guid memberId) : IQuery<MemberResponse>;

    
}
