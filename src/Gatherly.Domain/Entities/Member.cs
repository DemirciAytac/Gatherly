using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Entities
{
    public sealed class Member : AggregateRoot, IAuditableEntity
    {
        private readonly List<Address> _addresses = new();
        public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

        //EF Core need a parameterless constructor
        private Member() { }
        private Member(Guid id, Email email,FirstName firstName, LastName lastName) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        // private set => it is created first time in the constructer. Than you can't change it.
        public FirstName FirstName { get;private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public static Member Create(
            Guid id, 
            Email email, 
            FirstName firstName, 
            LastName lastName)
        {
            var member = new Member(id, email, firstName, lastName);
            member.RaiseDomainEvent(new MemberRegisteredDomainEvent(Guid.NewGuid(), member.Id));
            return member;
        }

        public void ChangeName(FirstName firstName, LastName lastName)
        {
            FirstName = firstName;
            LastName = lastName;

        }

        public void AddAddress(Address address)
        {
            _addresses.Add(address);
        }
    }
}
