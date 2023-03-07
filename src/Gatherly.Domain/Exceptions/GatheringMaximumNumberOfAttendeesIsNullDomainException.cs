using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Exceptions
{
    public sealed class GatheringMaximumNumberOfAttendeesIsNullDomainException : DomainExceptions
    {
        public GatheringMaximumNumberOfAttendeesIsNullDomainException(string message):base(message) 
        {

        }
    }
}
