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
        public DbSet<Group> Groups { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Group>().ToTable("Group");

            builder.Entity<Group>()
                .HasOne(g => g.Trainer)
                .WithMany(c => c.Groups)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Group>()
                .HasMany(g => g.Schedules)
                .WithOne(s => s.Group)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                .HasMany(g => g.Customers)
                .WithMany(c => c.Groups);

            builder.Entity<Schedule>().ToTable("Schedule");
            builder.Entity<Appointment>().ToTable("Appointment");
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

