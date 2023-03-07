using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Persistence.Repository
{
    public sealed class AttendeRepository : IAttendeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AttendeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Attendee attendee)
        {
            _dbContext.Set<Attendee>().Add(attendee);
        }
    }
}
