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
        public DbSet<Student> Students { get; set; }
        public DbSet<StudProjLink> StudProjLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sprint>()
                .HasOne(e => e.Project)
                .WithMany(e => e.Sprints)
                .HasForeignKey(e => e.ProjectId)
                .IsRequired();

            modelBuilder.Entity<Ticket>()
                .HasOne(e => e.Sprint)
                .WithMany(e => e.Tickets)
                .HasForeignKey(e => e.SprintId)
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

            modelBuilder.Entity<Student>()
                .HasOne(e => e.School)
                .WithMany(e => e.Students)
                .HasForeignKey(e => e.SchoolId)
                .IsRequired();

            modelBuilder.Entity<StudProjLink>()
                .HasOne(e => e.Student)
                .WithMany(e => e.StudProjLinks)
                .HasForeignKey(e => e.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudProjLink>()
                .HasOne(e => e.Project)
                .WithMany(e => e.StudProjLinks)
                .HasForeignKey(e => e.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
