using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Syllabus.Domain.Sylabusses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus.Infrastructure.Data.Configuration
{
    public class CourseDetailConfiguration : IEntityTypeConfiguration<CourseDetail>
    {
        public void Configure(EntityTypeBuilder<CourseDetail> builder)
        {
            builder.ToTable("CourseDetails");

            builder.HasKey(cd => cd.Id);

            builder.Property(cd => cd.AcademicProgram)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(cd => cd.AcademicYear)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(cd => cd.Language)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cd => cd.CourseTypeLabel)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cd => cd.EthicsCode)
                .IsRequired();

            builder.Property(cd => cd.ExamMethod)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cd => cd.TeachingFormat)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cd => cd.Credits)
                .IsRequired();

            builder.Property(cd => cd.Objective)
                .IsRequired();

            builder.Property(cd => cd.KeyConcepts)
                .IsRequired();

            builder.Property(cd => cd.Prerequisites)
                .IsRequired();

            builder.Property(cd => cd.SkillsAcquired)
                .IsRequired();

            builder.Property(cd => cd.CourseResponsible)
                .HasMaxLength(255);

            builder.HasOne(cd => cd.Course)
                   .WithOne(c => c.Detail)
                   .HasForeignKey<CourseDetail>(cd => cd.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(cd => cd.TeachingPlan, tp =>
            {
                tp.Property(t => t.LectureHours).IsRequired();
                tp.Property(t => t.LabHours).IsRequired();
                tp.Property(t => t.PracticeHours).IsRequired();
                tp.Property(t => t.ExerciseHours).IsRequired();
                tp.Property(t => t.WeeklyHours).IsRequired();
                tp.Property(t => t.IndividualStudyHours).IsRequired();
            });

            builder.OwnsOne(cd => cd.EvaluationBreakdown, eb =>
            {
                eb.Property(e => e.ParticipationPercent).IsRequired();
                eb.Property(e => e.Test1Percent).IsRequired();
                eb.Property(e => e.Test2Percent).IsRequired();
                eb.Property(e => e.Test3Percent).IsRequired();
                eb.Property(e => e.FinalExamPercent).IsRequired();
            });

            builder.HasMany(cd => cd.Topics)
                   .WithOne(t => t.CourseDetail)
                   .HasForeignKey(t => t.CourseDetailId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
