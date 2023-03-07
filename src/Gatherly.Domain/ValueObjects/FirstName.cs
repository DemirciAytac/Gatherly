using Gatherly.Domain.Exceptions;
using Gatherly.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.ValueObjects
{
    public sealed class FirstName : ValueObject
    {
        private FirstName() { }
        public const int MaxLength = 50;
        public string Value { get; }

        private FirstName(string value)
        {

            Value = value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;   
            //Bu value objectin extra propertyleri olursa buraya eklenecek.
        }

        public static FirstName Create(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentNullException($"{nameof(firstName)} is empty.");
            }

            if (firstName.Length > MaxLength)
            {
                throw new InvalidLenghtException($"{nameof(firstName)} is too long.");
            }
            
            return new FirstName(firstName);
        }
    }
}
