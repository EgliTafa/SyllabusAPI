using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class SyllabusEntityConfiguration : IEntityTypeConfiguration<Sylabus>
    {
        public void Configure(EntityTypeBuilder<Sylabus> builder)
        {
            builder.ToTable("Syllabuses");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.ProgramAcademicYearId);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.UpdatedAt);

            // Relationships
            builder.HasOne(s => s.ProgramAcademicYear)
                .WithMany(pay => pay.Syllabuses)
                .HasForeignKey(s => s.ProgramAcademicYearId)
                .OnDelete(DeleteBehavior.Restrict);

            //one syllabus has many courses
            builder.HasMany(s => s.Courses)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade); //cascade deletes
        }
    }
}
