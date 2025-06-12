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

            builder.Property(s => s.AcademicYear)
                .IsRequired()
                .HasMaxLength(20);

            //one syllabus has many courses
            builder.HasMany(s => s.Courses)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade); //cascade deletes
        }
    }
}
