using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Persistence.Repository
{
    internal sealed class GatheringRepository : IGatheringRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GatheringRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Gathering gathering)
        {
          _dbContext.Set<Gathering>().Add(gathering);   
        }

        public async Task<Gathering> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
          Gathering? gathering =  await _dbContext.Set<Gathering>()
                                                  //.AsSingleQuery()
                                                  .Include(gathering => gathering.Creator)
                                                  .Include(gathering => gathering.Invitations)
                                                  .Include(gathering => gathering.Attendees)
                                                  .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return gathering;
        }

        public async Task<Gathering> GetByIdWithCInvitationsAsync(Guid id, CancellationToken cancellationToken)
        {
            Gathering? gathering =  await _dbContext.Set<Gathering>()
                                                    .Include(g => g.Invitations)
                                                    .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

            return gathering;
        }

        public async Task<Gathering> GetByIdWithCreatorAsync(Guid id, CancellationToken cancellationToken)
        {
            Gathering? gathering =  await _dbContext.Set<Gathering>()
                                                    .Include(gathering => gathering.Creator)
                                                    .FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

            return gathering;
        }

        public void Remove(Gathering gathering)
        {
            _dbContext.Set<Gathering>().Remove(gathering);
        }
    }
}
