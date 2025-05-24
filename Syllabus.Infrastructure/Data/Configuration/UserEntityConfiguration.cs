using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syllabus.Domain.Users;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .HasMaxLength(255);

            builder.Property(e => e.EmailVerified);

            builder.Property(e => e.ProfilePictureUrl)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(e => e.Status)
                .HasConversion<string>();

            builder.OwnsOne(e => e.PhoneNumberInfo, b =>
            {
                b.Property(p => p.Prefix)
                    .HasColumnName("PhonePrefix")
                    .HasMaxLength(10)
                    .IsRequired();

                b.Property(p => p.Number)
                    .HasColumnName("PhoneNumber")
                    .HasMaxLength(20)
                    .IsRequired();

                b.HasIndex(p => new { p.Prefix, p.Number }).IsUnique();
            });
        }
    }
}
