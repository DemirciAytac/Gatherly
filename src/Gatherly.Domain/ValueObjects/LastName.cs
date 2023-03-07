using Gatherly.Domain.Exceptions;
using Gatherly.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.ValueObjects
{
    public sealed class LastName : ValueObject
    {
        private LastName() { }
        public const int MaxLength = 50;
        public string Value { get; }

        private LastName(string value)
        {

            Value = value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            //Bu value objectin extra propertyleri olursa buraya eklenecek.
        }

        public static LastName Create(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentNullException($"{nameof(lastName)} is empty.");
            }

            if (lastName.Length > MaxLength)
            {
                throw new InvalidLenghtException($"{nameof(lastName)} is too long.");
            }

            return new LastName(lastName);
        }
    }
}
