﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(WasteContext))]
    [Migration("20241030152757_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Entities.Community", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TagNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("Domain.Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AuthorizedSignature")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("FundingAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProjectDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Domain.Entities.GovernmentAgent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ContractId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SectorName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("GovernmentAgents");
                });

            modelBuilder.Entity("Domain.Entities.Individual", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Individuals");
                });

            modelBuilder.Entity("Domain.Entities.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4dd715d0-61b0-4a15-998a-3e3412d438b1"),
                            DateCreated = new DateTime(2024, 10, 30, 15, 27, 49, 282, DateTimeKind.Utc).AddTicks(8543),
                            IsActive = true,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("StaffNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("Domain.Entities.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CurrentPlanDateEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CurrentPlanDateStart")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CurrentPlanType")
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3cdd30b0-0234-4ce7-b7be-7708d7bb4ad6"),
                            Email = "admin@gmail.com",
                            FullName = "Admin",
                            IsActive = true,
                            Password = "$2a$11$zFlYlj4lxWLKn8kHFLvcLO6PZQzkpPG2RNEE3twqORrWPkV.dmvAW",
                            RoleId = new Guid("4dd715d0-61b0-4a15-998a-3e3412d438b1")
                        });
                });

            modelBuilder.Entity("Domain.Entities.WasteCollection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CommunityId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("GovernmentAgentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("IndividualId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("StaffId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.HasIndex("GovernmentAgentId");

                    b.HasIndex("IndividualId");

                    b.HasIndex("StaffId");

                    b.ToTable("WasteCollections");
                });

            modelBuilder.Entity("Domain.Entities.WasteReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CommunityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("GovernmentAgentId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("IndividualId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("WasteCollectionId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.HasIndex("GovernmentAgentId");

                    b.HasIndex("IndividualId");

                    b.HasIndex("WasteCollectionId")
                        .IsUnique();

                    b.ToTable("WasteReports");
                });

            modelBuilder.Entity("Domain.Entities.Community", b =>
                {
                    b.HasOne("Domain.Entities.Subscription", "Subscription")
                        .WithMany("Communities")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Domain.Entities.GovernmentAgent", b =>
                {
                    b.HasOne("Domain.Entities.Contract", "Contract")
                        .WithMany("GovernmentAgents")
                        .HasForeignKey("ContractId");

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("Domain.Entities.Individual", b =>
                {
                    b.HasOne("Domain.Entities.Subscription", "Subscription")
                        .WithMany("Individuals")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.WasteCollection", b =>
                {
                    b.HasOne("Domain.Entities.Community", "Community")
                        .WithMany("WasteCollections")
                        .HasForeignKey("CommunityId");

                    b.HasOne("Domain.Entities.GovernmentAgent", "GovernmentAgent")
                        .WithMany("WasteCollections")
                        .HasForeignKey("GovernmentAgentId");

                    b.HasOne("Domain.Entities.Individual", null)
                        .WithMany("WasteCollections")
                        .HasForeignKey("IndividualId");

                    b.HasOne("Domain.Entities.Staff", "Staff")
                        .WithMany("WasteCollections")
                        .HasForeignKey("StaffId");

                    b.Navigation("Community");

                    b.Navigation("GovernmentAgent");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Domain.Entities.WasteReport", b =>
                {
                    b.HasOne("Domain.Entities.Community", "Community")
                        .WithMany("WasteReports")
                        .HasForeignKey("CommunityId");

                    b.HasOne("Domain.Entities.GovernmentAgent", "GovernmentAgent")
                        .WithMany()
                        .HasForeignKey("GovernmentAgentId");

                    b.HasOne("Domain.Entities.Individual", null)
                        .WithMany("WasteReports")
                        .HasForeignKey("IndividualId");

                    b.HasOne("Domain.Entities.WasteCollection", "WasteCollection")
                        .WithOne("WasteReport")
                        .HasForeignKey("Domain.Entities.WasteReport", "WasteCollectionId");

                    b.Navigation("Community");

                    b.Navigation("GovernmentAgent");

                    b.Navigation("WasteCollection");
                });

            modelBuilder.Entity("Domain.Entities.Community", b =>
                {
                    b.Navigation("WasteCollections");

                    b.Navigation("WasteReports");
                });

            modelBuilder.Entity("Domain.Entities.Contract", b =>
                {
                    b.Navigation("GovernmentAgents");
                });

            modelBuilder.Entity("Domain.Entities.GovernmentAgent", b =>
                {
                    b.Navigation("WasteCollections");
                });

            modelBuilder.Entity("Domain.Entities.Individual", b =>
                {
                    b.Navigation("WasteCollections");

                    b.Navigation("WasteReports");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Entities.Staff", b =>
                {
                    b.Navigation("WasteCollections");
                });

            modelBuilder.Entity("Domain.Entities.Subscription", b =>
                {
                    b.Navigation("Communities");

                    b.Navigation("Individuals");
                });

            modelBuilder.Entity("Domain.Entities.WasteCollection", b =>
                {
                    b.Navigation("WasteReport");
                });
#pragma warning restore 612, 618
        }
    }
}
