using Gatherly.Domain.Primitives;
using Gatherly.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string City { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }

        private Address() { } // EF Core için

        public Address(string city, string street, string zipCode)
        {
            City = city;
            Street = street;
            ZipCode = zipCode;
        }


        public override IEnumerable<object> GetAtomicValues()
        {
            yield return City;
            yield return Street;
            yield return ZipCode;
        }

        public static Result<Address> Create(string city, string street, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Result.Failure<Address>(new Error("City.Emty", "City is empty"));
            }

            return new Address(city, street, zipCode);
        }
    }
}
