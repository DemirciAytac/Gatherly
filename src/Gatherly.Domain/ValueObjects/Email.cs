using Gatherly.Domain.Exceptions;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        private Email() { }
        public const int MaxLength = 50;
        public string Value { get; }

        private Email(string value)
        {

            Value = value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            //Bu value objectin extra propertyleri olursa buraya eklenecek.
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<Email>(new Error("Email.Emty", "Email is empty"));
            }

            if (email.Length > MaxLength)
            {
                return Result.Failure<Email>(new Error("Email.TooLong", "Email is too long."));
            }

            return new Email(email);
        }
    }
}
