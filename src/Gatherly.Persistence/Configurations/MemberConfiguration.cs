using Gatherly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Persistence.Configurations
{
    internal class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {

            builder.OwnsOne(o => o.FirstName, firstname =>
            {
                firstname.Property(x => x.Value).HasColumnName("FirstName");
            });

            builder.OwnsOne(o => o.LastName, lastname =>
            {
                lastname.Property(x => x.Value).HasColumnName("LastName");
            });

            builder.OwnsOne(o => o.Email, email =>
            {
                email.Property(x => x.Value).HasColumnName("Email");
            });

            builder.OwnsMany(c => c.Addresses, a =>
            {
                a.WithOwner().HasForeignKey("MemberId");
                a.Property<Guid>("Id"); // Shadow primary key
                a.HasKey("Id");
                a.ToTable("MemberAddresses");
            });
        }
    }
}
