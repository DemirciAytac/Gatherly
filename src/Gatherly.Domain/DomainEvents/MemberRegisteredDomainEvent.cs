using Gatherly.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.DomainEvents
{
    public sealed record MemberRegisteredDomainEvent(Guid Id, Guid MemberId):DomainEvent(Id);

}
