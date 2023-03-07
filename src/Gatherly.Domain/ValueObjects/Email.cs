using Gatherly.Domain.Exceptions;
using Gatherly.Domain.Primitives;
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

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException($"{nameof(email)} is empty.");
            }

            if (email.Length > MaxLength)
            {
                throw new InvalidLenghtException($"{nameof(email)} is too long.");
            }

            return new Email(email);
        }
    }
}
