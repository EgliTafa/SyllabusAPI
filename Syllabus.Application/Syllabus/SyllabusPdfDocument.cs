using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Syllabus
{

    public class SyllabusPdfDocument : IDocument
    {
        private readonly Sylabus _syllabus;
        private readonly string _logoPath;

        public SyllabusPdfDocument(Sylabus syllabus, string logoPath)
        {
            _syllabus = syllabus ?? throw new ArgumentNullException(nameof(syllabus));
            _logoPath = logoPath;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(11));
                page.Header().Element(Header);
                page.Content().Element(Content);
            });
        }

        private void Header(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("FAKULTETI I SHKENCAVE TË NATYRËS").Bold().FontSize(14);
                    col.Item().Text("DEPARTAMENTI I INFORMATIKËS").FontSize(12);
                    col.Item().Text($"Programi: {_syllabus.Name}").FontSize(12);
                });

                row.ConstantItem(70).Image(Image.FromFile(_logoPath)).FitArea();
            });
        }

        private void Content(IContainer container)
        {
            foreach (var course in _syllabus.Courses)
            {
                var detail = course.Detail;
                if (detail == null) continue;

                container
                    .PaddingTop(20)
                    .Column(col =>
                    {
                        col.Item().Text($"{course.Code} - {course.Title}").Bold().FontSize(13);

                        col.Item().Text($"Akademik: {detail.AcademicYear}, Program: {detail.AcademicProgram}");
                        col.Item().Text($"Gjuha: {detail.Language}  | Kredite: {detail.Credits} | Tipologjia: {detail.CourseTypeLabel}");
                        col.Item().Text($"Mënyra e provimit: {detail.ExamMethod} - {detail.TeachingFormat}");

                        col.Item().PaddingTop(10).Text("Orë Mësimore").Bold();
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(50);
                            });

                            table.Cell().Text("Leksione"); table.Cell().Text(course.LectureHours.ToString());
                            table.Cell().Text("Seminar"); table.Cell().Text(course.SeminarHours.ToString());
                            table.Cell().Text("Laborator"); table.Cell().Text(course.LabHours.ToString());
                            table.Cell().Text("Totali"); table.Cell().Text(course.TotalHours.ToString());
                        });

                        col.Item().PaddingTop(10).Text("Vlerësimi").Bold();
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(50);
                            });

                            table.Cell().Text("Pjesëmarrje"); table.Cell().Text($"{detail.EvaluationBreakdown.ParticipationPercent}%");
                            table.Cell().Text("Test 1"); table.Cell().Text($"{detail.EvaluationBreakdown.Test1Percent}%");
                            table.Cell().Text("Test 2"); table.Cell().Text($"{detail.EvaluationBreakdown.Test2Percent}%");
                            table.Cell().Text("Provimi Final"); table.Cell().Text($"{detail.EvaluationBreakdown.FinalExamPercent}%");
                            table.Cell().Text("Total"); table.Cell().Text("100%");
                        });

                        col.Item().PaddingTop(10).Text("Përshkrimi dhe Objektivat").Bold();
                        col.Item().Text(detail.Objective);
                        col.Item().PaddingTop(5).Text("Njohuri Paraprake").Bold();
                        col.Item().Text(detail.Prerequisites);
                        col.Item().PaddingTop(5).Text("Aftësitë e Fitura").Bold();
                        col.Item().Text(detail.SkillsAcquired);

                        col.Item().PaddingTop(10).Text("Temat").Bold();
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(30);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Nr.").Bold();
                                header.Cell().Text("Titulli").Bold();
                                header.Cell().Text("Referenca").Bold();
                            });

                            int i = 1;
                            foreach (var topic in detail.Topics ?? Enumerable.Empty<Topic>())
                            {
                                table.Cell().Text(i++.ToString());
                                table.Cell().Text(topic.Title);
                                table.Cell().Text(topic.Reference ?? "-");
                            }
                        });

                        if (!string.IsNullOrWhiteSpace(detail.CourseResponsible))
                        {
                            col.Item().PaddingTop(20).AlignRight().Text($"Përgjegjës i lëndës: {detail.CourseResponsible}");
                        }

                        col.Item().PageBreak(); // Separate each course
                    });
            }
        }
    }
}
