using IJobs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Data
{
    public class projectContext : DbContext
    {
        //one to many
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Subdomain> Subdomains { get; set; }
        public DbSet<Tutorial> Tutorials { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public projectContext(DbContextOptions<projectContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Domain>().HasIndex(x => x.Name).IsUnique();

            //one to many
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Jobs)
                .WithOne(j => j.Company);

            modelBuilder.Entity<Application>()
                .HasMany(c => c.Interviews)
                .WithOne(j => j.Application);

            modelBuilder.Entity<Domain>()
                .HasMany(c => c.Subdomains)
                .WithOne(j => j.Domain);

            modelBuilder.Entity<Subdomain>()
                .HasMany(c => c.Tutorials)
                .WithOne(j => j.Subdomain);

            modelBuilder.Entity<Subdomain>()
                .HasMany(c => c.Jobs)
                .WithOne(j => j.Subdomain);


            //many to many
            modelBuilder.Entity<Application>().HasKey(app => new { app.UserId, app.JobId });

            modelBuilder.Entity<Application>()
                   .HasOne<User>(app => app.User)
                   .WithMany(u => u.Applications)
                   .HasForeignKey(app => app.UserId);


            modelBuilder.Entity<Application>()
                   .HasOne<Job>(app => app.Job)
                   .WithMany(j => j.Applications)
                   .HasForeignKey(app => app.JobId);




            base.OnModelCreating(modelBuilder);
        }
    }
}