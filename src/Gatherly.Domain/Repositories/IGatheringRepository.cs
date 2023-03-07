using Gatherly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Repositories
{
    public interface IGatheringRepository
    {
        Task<Gathering> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Gathering> GetByIdWithCreatorAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Gathering> GetByIdWithCInvitationsAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Gathering gathering);
        void Remove(Gathering gathering);
    }
}
