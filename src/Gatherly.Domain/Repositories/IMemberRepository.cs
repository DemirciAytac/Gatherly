using Gatherly.Domain.Entities;
using Gatherly.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Repositories
{
    public interface IMemberRepository
    {
        Task<Member> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
        void Add(Member member);   
        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);
    }
}
