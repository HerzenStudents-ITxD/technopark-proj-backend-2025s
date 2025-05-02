using Microsoft.EntityFrameworkCore;
using TechnoparkProj.Core.Models;
using TechnoparkProj.DataAccess.Entities;

namespace TechnoparkProj.DataAccess
{
    public class TechnoparkProjDbContext : DbContext
    {
        protected TechnoparkProjDbContext()
            : base()
        {
        }

        public TechnoparkProjDbContext(DbContextOptions<TechnoparkProjDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Institute> Institutes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(e => e.Project)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.ProjectID)
                .IsRequired();

            modelBuilder.Entity<Ticket>()
                .HasOne(e => e.Sprint)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.SprintID)
                .IsRequired();

            modelBuilder.Entity<School>()
                .HasOne(e => e.Institute)
                .WithMany(e => e.Schools)
                .HasForeignKey(e => e.InstituteId)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .HasOne(e => e.School)
                .WithMany(e => e.Projects)
                .HasForeignKey(e => e.SchoolId)
                .IsRequired();
        }

    }
}
