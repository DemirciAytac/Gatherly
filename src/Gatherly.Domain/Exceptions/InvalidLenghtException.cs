using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Exceptions
{
    public sealed class InvalidLenghtException : DomainExceptions
    {
        public InvalidLenghtException(string message):base(message)
        {

        }
    }
}
