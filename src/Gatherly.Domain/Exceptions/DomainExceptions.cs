using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Exceptions
{
    public abstract class DomainExceptions : Exception
    {
        public DomainExceptions(string message):base(message)
        {

        }
    }
}
