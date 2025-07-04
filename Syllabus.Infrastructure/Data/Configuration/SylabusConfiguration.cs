using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class SylabusConfiguration : IEntityTypeConfiguration<Sylabus>
    {
        public void Configure(EntityTypeBuilder<Sylabus> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.HasOne(s => s.ProgramAcademicYear)
                .WithMany(pay => pay.Syllabuses)
                .HasForeignKey(s => s.ProgramAcademicYearId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 