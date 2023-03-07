using Gatherly.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Invitations.Queries.GetInvitationById
{
    public sealed record InvitationResponse(Guid Id, InvitationStatus Status);

}
