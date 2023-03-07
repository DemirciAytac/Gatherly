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
        //EF Core need a parameterless constructor
        private Member() { }
        private Member(Guid id, Email email,FirstName firstName, LastName lastName) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }


        public FirstName FirstName { get;private set; }
        public LastName LastName { get; private set; }
        public Email Email { get; private set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }

        public static Member Create(
            Guid id, 
            Email email, 
            FirstName firstName, 
            LastName lastName, 
            bool isEmailUniqueAsync)
        {
            if(!isEmailUniqueAsync)
            {
                return null; // log or throw exception
            }
            var member = new Member(id, email, firstName, lastName);
            member.RaiseDomainEvent(new MemberRegisteredDomainEvent(Guid.NewGuid(), member.Id));
            return member;
        }

        public void ChangeName(FirstName firstName, LastName lastName)
        {
            FirstName = firstName;
            LastName = lastName;

        }
    }
}
