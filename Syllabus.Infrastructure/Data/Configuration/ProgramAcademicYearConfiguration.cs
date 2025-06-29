using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class ProgramAcademicYearConfiguration : IEntityTypeConfiguration<ProgramAcademicYear>
    {
        public void Configure(EntityTypeBuilder<ProgramAcademicYear> builder)
        {
            builder.HasKey(pay => pay.Id);
            builder.Property(pay => pay.AcademicYear)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasOne(pay => pay.Program)
                .WithMany(p => p.AcademicYears)
                .HasForeignKey(pay => pay.ProgramId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(pay => pay.Syllabuses)
                .WithOne(s => s.ProgramAcademicYear)
                .HasForeignKey(s => s.ProgramAcademicYearId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(pay => new { pay.ProgramId, pay.AcademicYear }).IsUnique();
        }
    }
} 