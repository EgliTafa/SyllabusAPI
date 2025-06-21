using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Year)
                .IsRequired();

            builder.Property(c => c.Semester)
                .IsRequired();

            builder.Property(c => c.LectureHours)
                .IsRequired();

            builder.Property(c => c.SeminarHours)
                .IsRequired();

            builder.Property(c => c.LabHours)
                .IsRequired();

            builder.Property(c => c.PracticeHours)
                .IsRequired();

            builder.Property(c => c.Credits)
                .IsRequired();

            builder.Property(c => c.Evaluation)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(c => c.Type)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(c => c.ElectiveGroup)
                .HasMaxLength(50);

            builder.Ignore(c => c.TotalHours); // calculated property, not mapped
        }
    }
}
