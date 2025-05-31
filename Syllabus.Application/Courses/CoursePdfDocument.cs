using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Courses
{
    public class CoursePdfDocument : IDocument
    {
        private readonly Course _course;
        private readonly string _logoPath;

        // Static info for footer and header
        private const string University = "FAKULTETI I SHKENCAVE TË NATYRËS";
        private const string Department = "DEPARTAMENTI I INFORMATIKËS";
        private const string Address = "Adresa: Bulevardi 'Zogu I', Nr. 25/1, Tiranë, Tel. & Fax: +355 4 2229560, www.fshn.edu.al";
        private const string Website = "www.fshn.edu.al";
        private const string DepartmentHead = "Prof. Alda Kika";
        private const string CourseResponsible = "Julian Fejzaj";

        public CoursePdfDocument(Course course, string logoPath)
        {
            _course = course ?? throw new ArgumentNullException(nameof(course));
            _logoPath = logoPath ?? throw new ArgumentNullException(nameof(logoPath));
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.Content().Element(BuildDocument);
            });
        }

        private void BuildDocument(IContainer container)
        {
            var detail = _course.Detail;
            container.Column(col =>
            {
                // Header
                col.Item().Element(HeaderSection);
                col.Item().PaddingTop(10).Element(c => CourseTitleSection(c, _course));
                col.Item().PaddingTop(10).Element(c => TeachingActivityTable(c, _course, detail));
                col.Item().PaddingTop(10).Element(c => EvaluationSection(c, detail));
                col.Item().PaddingTop(10).Element(c => ObjectivesSection(c, detail));
                col.Item().PaddingTop(10).Element(c => TopicsSection(c, detail));
                col.Item().PaddingTop(10).Element(LiteratureSection);
                col.Item().PaddingTop(20).Element(FooterSection);
            });
        }

        private void HeaderSection(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().AlignCenter().Height(70).Image(Image.FromFile(_logoPath));
                col.Item().AlignCenter().Text(University).FontSize(14).Bold();
                col.Item().AlignCenter().Text(Department).FontSize(12).Bold();
                col.Item().AlignCenter().Text($"CIKLI I BACHELOR INFORMATIKË").FontSize(12).Bold();
                col.Item().AlignCenter().Text($"PROGRAMI I LËNDËS: RRJETAT").FontSize(12).Bold();
                col.Item().AlignCenter().Text($"VITI AKADEMIK : 2028-2029").FontSize(12).Bold();
            });
        }

        private void CourseTitleSection(IContainer container, Course course)
        {
            container.Column(col =>
            {
                col.Item().Text($"Kodi: {course.Code} - {course.Title}").Bold().FontSize(12);
            });
        }

        private void TeachingActivityTable(IContainer container, Course? course, CourseDetail? detail)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2); // Activity
                    columns.RelativeColumn(); // Lectures
                    columns.RelativeColumn(); // Seminar
                    columns.RelativeColumn(); // Labs
                    columns.RelativeColumn(); // Practice
                    columns.RelativeColumn(); // Total
                });

                // Header row
                table.Header(header =>
                {
                    header.Cell().Element(CellStyleHeader).Text("");
                    header.Cell().Element(CellStyleHeader).Text("Leksione");
                    header.Cell().Element(CellStyleHeader).Text("Seminar");
                    header.Cell().Element(CellStyleHeader).Text("Laboratore");
                    header.Cell().Element(CellStyleHeader).Text("Praktike");
                    header.Cell().Element(CellStyleHeader).Text("Totale");
                });

                if (course != null && detail != null)
                {
                    int total = course.LectureHours + course.SeminarHours + course.LabHours + 0; // Practice = 0
                    table.Cell().Element(CellStyle).Text("Orë mësimore");
                    table.Cell().Element(CellStyle).Text(course.LectureHours.ToString());
                    table.Cell().Element(CellStyle).Text(course.SeminarHours.ToString());
                    table.Cell().Element(CellStyle).Text(course.LabHours.ToString());
                    table.Cell().Element(CellStyle).Text("0");
                    table.Cell().Element(CellStyle).Text(total.ToString());
                }
            });
        }

        private void EvaluationSection(IContainer container, CourseDetail? detail)
        {
            container.Column(col =>
            {
                col.Item().Text("Vlerësimi dhe Kreditet").Bold().FontSize(12).FontColor(Colors.Red.Medium);
                if (detail != null)
                {
                    col.Item().Text($"Kredite (ECTS): {detail.Credits}").Bold();
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyleHeader).Text("Detyra");
                            header.Cell().Element(CellStyleHeader).Text("Pjesëmarrje");
                            header.Cell().Element(CellStyleHeader).Text("Detyra Kursi");
                            header.Cell().Element(CellStyleHeader).Text("Provimi Final");
                        });
                        table.Cell().Element(CellStyle).Text("% Vlerësimi");
                        table.Cell().Element(CellStyle).Text($"{detail.EvaluationBreakdown.ParticipationPercent}%");
                        table.Cell().Element(CellStyle).Text($"{detail.EvaluationBreakdown.Test1Percent + detail.EvaluationBreakdown.Test2Percent + detail.EvaluationBreakdown.Test3Percent}%");
                        table.Cell().Element(CellStyle).Text($"{detail.EvaluationBreakdown.FinalExamPercent}%");
                    });
                }
            });
        }

        private void ObjectivesSection(IContainer container, CourseDetail? detail)
        {
            container.Column(col =>
            {
                col.Item().Text("Përshkrimi dhe Objektivat").Bold().FontSize(12).FontColor(Colors.Red.Medium);
                if (detail != null)
                {
                    col.Item().Text($"Përshkrimi: {detail.Objective}");
                    col.Item().Text($"Objektivat: {detail.KeyConcepts}");
                    col.Item().Text($"Njohuri Paraprake: {detail.Prerequisites}");
                    col.Item().Text($"Aftësitë e Fitura: {detail.SkillsAcquired}");
                }
            });
        }

        private void TopicsSection(IContainer container, CourseDetail? detail)
        {
            container.Column(col =>
            {
                col.Item().Text("Tematika e Leksioneve").Bold().FontSize(12).FontColor(Colors.Red.Medium);
                if (detail?.Topics != null && detail.Topics.Any())
                {
                    int i = 1;
                    foreach (var topic in detail.Topics)
                    {
                        col.Item().Text($"{i++}. {topic.Title} ({topic.Hours} orë)");
                    }
                }
                // If you have exercise topics, add them here similarly
                col.Item().PaddingTop(10).Text("Tematika e Ushtrimeve").Bold().FontSize(12).FontColor(Colors.Red.Medium);
                // Example: If exercise topics are stored separately, list them here
            });
        }

        private void LiteratureSection(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Text("Literatura").Bold().FontSize(12).FontColor(Colors.Red.Medium);
                col.Item().Text("Mike Harwood\nCompTIA® Network+ (N10-007) Cert Guide,2018");
                col.Item().Text("Wendall Odom\nCCNA 200-301 Official Cert Guide, Volume 1");
                col.Item().Text("Will Panek\nMCSA Windows Server 2016 Study Guide: Exam 70-742");
            });
        }

        private void FooterSection(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Text($"Përgjegjësi i lëndës: {CourseResponsible}").FontColor(Colors.Red.Medium).Bold();
                col.Item().Text($"Përgjegjësi i Departamentit: {DepartmentHead}").FontColor(Colors.Red.Medium).Bold();
                col.Item().PaddingTop(10).Text(Address).FontSize(9).AlignCenter();
                col.Item().Text(Website).FontSize(9).AlignCenter().FontColor(Colors.Blue.Medium);
            });
        }

        private IContainer CellStyle(IContainer container) =>
            container.Border(1).Padding(2).AlignCenter();
        private IContainer CellStyleHeader(IContainer container) =>
            container.Background(Colors.Grey.Lighten2).Border(1).Padding(2).AlignCenter();
    }
} 