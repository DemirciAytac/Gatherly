using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Application.DTOs
{
    public record AddressDTO(string street, string city, string zipCode);
}
