using System;
using Microsoft.EntityFrameworkCore;
using Gym_API.Models;
using Gym_API.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Gym_API.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,string>
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Coach>().ToTable("Coach");
            builder.Entity<Supervisor>().ToTable("Supervisor");
            builder.Entity<Customer>().ToTable("Customer");
            builder.Entity<Gender>().ToTable("Genders").HasIndex(g => g.Name).IsUnique();
            builder.Entity<Specialization>().ToTable("Specializations").HasIndex(s => s.Name).IsUnique();
            builder.Entity<Status>().ToTable("Statuses").HasIndex(s => s.Name).IsUnique();

            base.OnModelCreating(builder);
        }

    }
}

