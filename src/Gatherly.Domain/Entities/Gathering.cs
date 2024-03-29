﻿using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Enums;
using Gatherly.Domain.Exceptions;
using Gatherly.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gatherly.Domain.Entities
{
    public sealed class Gathering : AggregateRoot
    {
        private readonly List<Invitation> _invitations = new();
        private readonly List<Attendee> _attendee = new();

        //it was described for EF Core
        private Gathering() { }
        private Gathering(Guid id, Member creator, GatheringType type, DateTime scheduledAtUtc, string name, string? location) : base(id)
        {
            Creator = creator;
            Type = type;           
            ScheduledAtUtc = scheduledAtUtc;
            Name = name;
            Location = location;
        }


        public Member Creator { get;private  set; }
        public GatheringType Type { get; private set; }
        public string Name { get; private set; }
        public DateTime ScheduledAtUtc { get; private set; }
        public string? Location { get; private set; }
        public int? MaximumNumberOfAttendees { get; private set; }
        public DateTime? InvitationsExpireAtUtc { get; private set; }
        public int NumberOfAttendees { get;  private set; }
        public IReadOnlyCollection<Attendee> Attendees => _attendee;
        public IReadOnlyCollection<Invitation> Invitations => _invitations;

        public static Gathering Create(
            Guid id, 
            Member creator, 
            GatheringType type, 
            DateTime scheduledAtUtc, 
            string name, 
            string? location,
            int? maximumNumberOfAttendees,
            int? invitationsValidBeforeInHours)
        {
            var gathering = new Gathering(
                Guid.NewGuid(),
                creator,
                type,
                scheduledAtUtc,
                name,
                location
                );

            gathering.CalculateGatheringTypeDetails(maximumNumberOfAttendees, invitationsValidBeforeInHours);
            return gathering;
        }

        private void CalculateGatheringTypeDetails(int? maximumNumberOfAttendees, int? invitationsValidBeforeInHours)
        {
            switch (Type)
            {
                case GatheringType.WithFixedNumberOfAttendees:
                    if (maximumNumberOfAttendees is null)
                    {
                        throw new GatheringMaximumNumberOfAttendeesIsNullDomainException($"{nameof(maximumNumberOfAttendees)} can't be null.");
                    }

                    MaximumNumberOfAttendees = maximumNumberOfAttendees;
                    break;
                case GatheringType.WithExpirationForInvitations:
                    if (invitationsValidBeforeInHours is null)
                    {
                        throw new GatheringInvitationsValidBeforeInHoursIsNullDomainException($"{nameof(invitationsValidBeforeInHours)} can't be null.");
                    }

                    InvitationsExpireAtUtc =
                        ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Gathering));
            }
        }

        public Invitation SendInvitation(Member member)
        {
            if (Creator.Id == member.Id)
            {
                throw new Exception("Can't send invitation to the gathering creator.");
            }

            if (ScheduledAtUtc < DateTime.UtcNow)
            {
                throw new Exception("Can't send invitation for gathering in the past.");
            }
            var invitation = new Invitation(Guid.NewGuid(), member, this);

            _invitations.Add(invitation);

            return invitation;
        }

        public Attendee? AcceptInvitation(Invitation invitation)
        {
            // Check if expired
            var expired = (Type == GatheringType.WithFixedNumberOfAttendees &&
                           NumberOfAttendees == MaximumNumberOfAttendees) ||
                          (Type == GatheringType.WithExpirationForInvitations &&
                           InvitationsExpireAtUtc < DateTime.UtcNow);
            if (expired)
            {
                invitation.Expired();

                return null;
            }

            var attendee = invitation.Accepted();

            RaiseDomainEvent(new InvitationAcceptedDomainEvent(Guid.NewGuid(), invitation.Id, Id));
            _attendee.Add(attendee);

            NumberOfAttendees++;

            return attendee;

        }

    }

   
}
