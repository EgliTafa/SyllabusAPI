using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(d => d.Description)
                .HasMaxLength(500);
                
            builder.Property(d => d.CreatedAt)
                .IsRequired();
                
            builder.Property(d => d.UpdatedAt);
            
            // Relationships
            builder.HasMany(d => d.Programs)
                .WithOne(p => p.Department)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Indexes
            builder.HasIndex(d => d.Name)
                .IsUnique();
        }
    }
} 