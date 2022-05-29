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
        public projectContext(DbContextOptions<projectContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            //one to many
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Jobs)
                .WithOne(j => j.Company);

            //one to one
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employment)
                .WithOne(e => e.User)
                .HasForeignKey<Employment>(e => e.UserId);

            //many to many
            modelBuilder.Entity<UserJobRelation>().HasKey(ujr => new { ujr.UserId, ujr.JobId });

            modelBuilder.Entity<UserJobRelation>()
                   .HasOne<User>(ujr => ujr.User)
                   .WithMany(u => u.UserJobRelations)
                   .HasForeignKey(ujr => ujr.UserId);


            modelBuilder.Entity<UserJobRelation>()
                   .HasOne<Job>(ujr => ujr.Job)
                   .WithMany(j => j.UserJobRelations)
                   .HasForeignKey(ujr => ujr.JobId);

            base.OnModelCreating(modelBuilder);
        }
    }
}