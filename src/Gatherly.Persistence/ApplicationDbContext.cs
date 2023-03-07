using Gatherly.Domain.Entities;
using Gatherly.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }       
        public DbSet<Gathering> Gathering { get; set; }
        public DbSet<Invitation> Invitation { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Attendee> Attende { get; set; }
        public DbSet<OutboxMessage> OutboxMessage { get; set; }
        public DbSet<OutboxMessageConsumer> OutboxMessageConsumer { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
