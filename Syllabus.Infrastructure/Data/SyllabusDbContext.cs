using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Syllabus.Domain.Sylabusses;
using Syllabus.Domain.Users;
using System;
using System.Reflection;

namespace Syllabus.Infrastructure.Data
{
    public class SyllabusDbContext : IdentityDbContext<UserEntity>
    {
        public SyllabusDbContext(DbContextOptions<SyllabusDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramAcademicYear> ProgramAcademicYears { get; set; }
        public virtual DbSet<Sylabus> Syllabuses { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseDetail> CourseDetails { get; set; }
        public virtual DbSet<Topic> CourseTopics { get; set; }

        // Expose the identity table
        public new virtual DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all IEntityTypeConfiguration<T> from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
