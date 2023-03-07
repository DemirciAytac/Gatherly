using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Persistence.Repository
{
    public sealed class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvitationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Invitation invitation)
        {
            _dbContext.Set<Invitation>().Add(invitation);
        }
    }
}
