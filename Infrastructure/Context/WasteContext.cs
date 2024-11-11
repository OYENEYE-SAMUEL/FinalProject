using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class WasteContext : DbContext
    {
        public WasteContext(DbContextOptions<WasteContext> opt) : base(opt) 
        {

        }
        public DbSet<Community> Communities => Set<Community>();
        public DbSet<Contract> Contracts => Set<Contract>();
        public DbSet<GovernmentAgent> GovernmentAgents => Set<GovernmentAgent>();
        public DbSet<Individual> Individuals => Set<Individual>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Staff> Staffs => Set<Staff>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<User> Users => Set<User>();
        public DbSet<WasteCollection> WasteCollections => Set<WasteCollection>();
        public DbSet<WasteReport> WasteReports => Set<WasteReport>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminUserId = Guid.NewGuid();
            var adminRoleId = Guid.NewGuid();

            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    DateCreated = DateTime.UtcNow,
                    IsActive = true
                });

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = adminUserId,
                    Email = "admin@gmail.com",
                    FullName = "Admin",
                    IsActive = true,
                    Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                    RoleId = adminRoleId
                });
        }

    }
}
