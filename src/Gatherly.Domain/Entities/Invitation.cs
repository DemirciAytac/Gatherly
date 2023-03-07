using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Entities
{
    public sealed class Invitation : Entity
    {
        private Invitation() { }
        internal Invitation(Guid id, Member member, Gathering gathering) : base(id)
        {
            MemberId = member.Id;
            GatheringId = gathering.Id;
            Status = InvitationStatus.Pending;
            CreatedOnUtc = DateTime.UtcNow;
        }

        public Guid MemberId { get; private set; }
        public Guid GatheringId { get; private set; }
        public InvitationStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ModifiedOnUtc { get; private set; }

        internal void Expired()
        {
            Status = InvitationStatus.Expired;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        internal Attendee Accepted()
        {
            Status = InvitationStatus.Accepted;
            ModifiedOnUtc = DateTime.UtcNow;

            var attendee = new Attendee(this);

            return attendee;
        }

    }
}
