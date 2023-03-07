using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.Attendees.Queries.GetAttendeById
{
    public sealed record AttendeeResponse(Guid MemberId, DateTime CreatedOnUtc);
}
