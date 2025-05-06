using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Persistence.Repository
{
    internal sealed class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Member member)
        {
            _dbContext.Set<Member>().Add(member);
        }

        public async Task<Member> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Member>().AsNoTracking().Include(x => x.Addresses).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Set<Member>().FirstOrDefaultAsync(x => x.Email.Value == email.Value);
            return result is  null ? true : false;
        }
    }
}
