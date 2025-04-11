using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("CourseTopics");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(t => t.Hours).IsRequired();

            builder.Property(t => t.Reference)
                   .HasMaxLength(500);
        }
    }
}
