using Gatherly.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities
{
    public sealed class Attendee
    {
        private Attendee() { }
        internal Attendee(Invitation invitation)
        {
            MemberId = invitation.MemberId; 
            GatheringId = invitation.GatheringId;
            CreatedOnUtc = DateTime.UtcNow;
        }
        public Guid MemberId { get; private set; }
        public Guid GatheringId { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
    }
}
