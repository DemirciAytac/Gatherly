﻿// <auto-generated />
using System;
using Gatherly.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gatherly.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250521203634_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gatherly.Domain.Entities.Attendee", b =>
                {
                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GatheringId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("MemberId", "GatheringId");

                    b.HasIndex("GatheringId");

                    b.ToTable("Attende");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Gathering", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("InvitationsExpireAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaximumNumberOfAttendees")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfAttendees")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduledAtUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Gathering");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GatheringId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GatheringId");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("Gatherly.Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessage");
                });

            modelBuilder.Entity("Gatherly.Persistence.Outbox.OutboxMessageConsumer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessageConsumer");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Attendee", b =>
                {
                    b.HasOne("Gatherly.Domain.Entities.Gathering", null)
                        .WithMany("Attendees")
                        .HasForeignKey("GatheringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Gathering", b =>
                {
                    b.HasOne("Gatherly.Domain.Entities.Member", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Invitation", b =>
                {
                    b.HasOne("Gatherly.Domain.Entities.Gathering", null)
                        .WithMany("Invitations")
                        .HasForeignKey("GatheringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Member", b =>
                {
                    b.OwnsMany("Gatherly.Domain.ValueObjects.Address", "Addresses", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<Guid>("MemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Id");

                            b1.HasIndex("MemberId");

                            b1.ToTable("MemberAddresses", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MemberId");
                        });

                    b.OwnsOne("Gatherly.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("MemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Email");

                            b1.HasKey("MemberId");

                            b1.ToTable("Member");

                            b1.WithOwner()
                                .HasForeignKey("MemberId");
                        });

                    b.OwnsOne("Gatherly.Domain.ValueObjects.FirstName", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("MemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("FirstName");

                            b1.HasKey("MemberId");

                            b1.ToTable("Member");

                            b1.WithOwner()
                                .HasForeignKey("MemberId");
                        });

                    b.OwnsOne("Gatherly.Domain.ValueObjects.LastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("MemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("LastName");

                            b1.HasKey("MemberId");

                            b1.ToTable("Member");

                            b1.WithOwner()
                                .HasForeignKey("MemberId");
                        });

                    b.Navigation("Addresses");

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();
                });

            modelBuilder.Entity("Gatherly.Domain.Entities.Gathering", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("Invitations");
                });
#pragma warning restore 612, 618
        }
    }
}
