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
        public DbSet<Invite> Invites { get; set; }
        public projectContext(DbContextOptions<projectContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Company>().Property(x => x.PasswordHash).IsRequired();
            modelBuilder.Entity<Company>().Property(x => x.verifiedAccount).IsRequired();

            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.PasswordHash).IsRequired();

            modelBuilder.Entity<Domain>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Domain>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Subdomain>().HasIndex(x => new {x.DomainId, x.Name}).IsUnique();
            modelBuilder.Entity<Subdomain>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Job>().Property(x => x.JobTitle).IsRequired();
            modelBuilder.Entity<Job>().Property(x => x.Description).IsRequired();
            modelBuilder.Entity<Job>().Property(x => x.Salary).IsRequired();
            modelBuilder.Entity<Job>().Property(x => x.JobType).IsRequired(); 
            modelBuilder.Entity<Job>().Property(x => x.Experience).IsRequired();
            modelBuilder.Entity<Job>().Property(x => x.Address).IsRequired();
            modelBuilder.Entity<Job>().Property(x => x.Open).IsRequired();

            modelBuilder.Entity<Tutorial>().Property(x => x.Link).IsRequired();

            modelBuilder.Entity<Application>().Property(x => x.CV).IsRequired();
            modelBuilder.Entity<Application>().Property(x => x.Status).IsRequired();
            modelBuilder.Entity<Application>().HasIndex(a => new { a.UserId, a.JobId }).IsUnique();

            modelBuilder.Entity<Invite>().HasIndex(a => new { a.UserId, a.JobId }).IsUnique();


            modelBuilder.Entity<Interview>().Property(x => x.Date).IsRequired();
            modelBuilder.Entity<Interview>().Property(x => x.IsOnline).IsRequired();
            modelBuilder.Entity<Interview>().Property(x => x.Location).IsRequired();
            modelBuilder.Entity<Interview>().Property(x => x.ResponseUser).IsRequired();
            modelBuilder.Entity<Interview>().Property(x => x.ResponseCompany).IsRequired();

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
                .WithOne(j => j.Subdomain)
                .OnDelete(DeleteBehavior.Cascade);


            //many to many

            modelBuilder.Entity<Application>()
                   .HasOne<User>(app => app.User)
                   .WithMany(u => u.Applications)
                   .HasForeignKey(app => app.UserId);

            modelBuilder.Entity<Application>()
                   .HasOne<Job>(app => app.Job)
                   .WithMany(j => j.Applications)
                   .HasForeignKey(app => app.JobId);


            modelBuilder.Entity<Invite>()
                   .HasOne<User>(inv => inv.User)
                   .WithMany(u => u.Invites)
                   .HasForeignKey(inv => inv.UserId);

            modelBuilder.Entity<Invite>()
                   .HasOne<Job>(inv => inv.Job)
                   .WithMany(j => j.Invites)
                   .HasForeignKey(inv => inv.JobId);


            base.OnModelCreating(modelBuilder);
        }
    }
}