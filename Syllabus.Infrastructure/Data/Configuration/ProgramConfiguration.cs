using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class ProgramConfiguration : IEntityTypeConfiguration<Program>
    {
        public void Configure(EntityTypeBuilder<Program> builder)
        {
            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(p => p.Description)
                .HasMaxLength(500);
                
            builder.Property(p => p.DepartmentId)
                .IsRequired();
                
            builder.Property(p => p.CreatedAt)
                .IsRequired();
                
            builder.Property(p => p.UpdatedAt);
            
            // Relationships
            builder.HasOne(p => p.Department)
                .WithMany(d => d.Programs)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasMany(p => p.AcademicYears)
                .WithOne(pay => pay.Program)
                .HasForeignKey(pay => pay.ProgramId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Indexes
            builder.HasIndex(p => p.Name)
                .IsUnique();
        }
    }
} 