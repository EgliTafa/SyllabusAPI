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

            builder.Property(cd => cd.AcademicProgram).HasMaxLength(200).IsRequired();
            builder.Property(cd => cd.AcademicYear).HasMaxLength(50).IsRequired();
            builder.Property(cd => cd.Language).HasMaxLength(50).IsRequired();
            builder.Property(cd => cd.CourseTypeLabel).HasMaxLength(100);
            builder.Property(cd => cd.EthicsCode).HasMaxLength(500);
            builder.Property(cd => cd.ExamMethod).HasMaxLength(100);
            builder.Property(cd => cd.TeachingFormat).HasMaxLength(100);
            builder.Property(cd => cd.Credits).IsRequired();

            builder.Property(cd => cd.Objective).HasColumnType("nvarchar(max)");
            builder.Property(cd => cd.KeyConcepts).HasColumnType("nvarchar(max)");
            builder.Property(cd => cd.Prerequisites).HasColumnType("nvarchar(max)");
            builder.Property(cd => cd.SkillsAcquired).HasColumnType("nvarchar(max)");

            builder.HasOne(cd => cd.Course)
                   .WithOne(c => c.Detail)
                   .HasForeignKey<CourseDetail>(cd => cd.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(cd => cd.TeachingPlan, tp =>
            {
                tp.Property(t => t.LectureHours).HasColumnName("LectureHours");
                tp.Property(t => t.LabHours).HasColumnName("LabHours");
                tp.Property(t => t.PracticeHours).HasColumnName("PracticeHours");
                tp.Property(t => t.ExerciseHours).HasColumnName("ExerciseHours");
                tp.Property(t => t.WeeklyHours).HasColumnName("WeeklyHours");
                tp.Property(t => t.IndividualStudyHours).HasColumnName("IndividualStudyHours");
            });

            builder.OwnsOne(cd => cd.EvaluationBreakdown, eb =>
            {
                eb.Property(e => e.ParticipationPercent).HasColumnName("Participation");
                eb.Property(e => e.Test1Percent).HasColumnName("Test1");
                eb.Property(e => e.Test2Percent).HasColumnName("Test2");
                eb.Property(e => e.Test3Percent).HasColumnName("Test3");
                eb.Property(e => e.FinalExamPercent).HasColumnName("FinalExam");
            });

            builder.HasMany(cd => cd.Topics)
                   .WithOne(t => t.CourseDetail)
                   .HasForeignKey(t => t.CourseDetailId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
