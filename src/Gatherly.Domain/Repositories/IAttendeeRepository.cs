using Gatherly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Repositories
{
    public interface IAttendeeRepository
    {
        void Add(Attendee attendee);
    }
}
