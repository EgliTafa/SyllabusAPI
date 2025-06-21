using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Syllabus.Domain.Sylabusses;
using System.Linq;
using System.Collections.Generic;

namespace Syllabus.Application.Syllabus
{
    public class SyllabusPdfDocument : IDocument
    {
        private readonly Sylabus _syllabus;
        private readonly string _logoPath;

        // Static info for footer and header
        private const string University = "FAKULTETI I SHKENCAVE TË NATYRËS";
        private const string Department = "DEPARTAMENTI I INFORMATIKËS";
        private const string Address = "Adresa: Bulevardi 'Zogu I', Nr. 25/1, Tiranë, Tel. & Fax: +355 4 2229560, www.fshn.edu.al";
        private const string Website = "www.fshn.edu.al";
        private const string DepartmentHead = "Prof. Alda Kika";
        private const string CourseResponsible = "Julian Fejzaj";

        public SyllabusPdfDocument(Sylabus syllabus, string logoPath)
        {
            _syllabus = syllabus ?? throw new ArgumentNullException(nameof(syllabus));
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
            container.Column(col =>
            {
                // Header (once at the top)
                col.Item().Element(HeaderSection);

                // Calculate totals
                var courses = _syllabus.Courses.ToList();
                var totals = new Dictionary<int, Dictionary<int, (int credits, int lecture, int seminar, int lab, int practice, int total)>>();
                var overall = (credits: 0, lecture: 0, seminar: 0, lab: 0, practice: 0, total: 0);
                foreach (var c in courses)
                {
                    if (!totals.ContainsKey(c.Year)) totals[c.Year] = new Dictionary<int, (int, int, int, int, int, int)>();
                    if (!totals[c.Year].ContainsKey(c.Semester)) totals[c.Year][c.Semester] = (0, 0, 0, 0, 0, 0);
                    var t = totals[c.Year][c.Semester];
                    t.credits += c.Credits;
                    t.lecture += c.LectureHours;
                    t.seminar += c.SeminarHours;
                    t.lab += c.LabHours;
                    t.practice += c.Detail?.TeachingPlan?.PracticeHours ?? 0;
                    t.total += c.LectureHours + c.SeminarHours + c.LabHours + (c.Detail?.TeachingPlan?.PracticeHours ?? 0);
                    totals[c.Year][c.Semester] = t;
                    overall.credits += c.Credits;
                    overall.lecture += c.LectureHours;
                    overall.seminar += c.SeminarHours;
                    overall.lab += c.LabHours;
                    overall.practice += c.Detail?.TeachingPlan?.PracticeHours ?? 0;
                    overall.total += c.LectureHours + c.SeminarHours + c.LabHours + (c.Detail?.TeachingPlan?.PracticeHours ?? 0);
                }

                // Summary Table
                col.Item().PaddingBottom(10).Element(c =>
                    c.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(); // Year
                            columns.RelativeColumn(); // Semester
                            columns.RelativeColumn(); // Credits
                            columns.RelativeColumn(); // Lecture
                            columns.RelativeColumn(); // Seminar
                            columns.RelativeColumn(); // Lab
                            columns.RelativeColumn(); // Practice
                            columns.RelativeColumn(); // Total
                        });
                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyleHeader).Text("Viti");
                            header.Cell().Element(CellStyleHeader).Text("Semestri");
                            header.Cell().Element(CellStyleHeader).Text("Kredite");
                            header.Cell().Element(CellStyleHeader).Text("Leksione");
                            header.Cell().Element(CellStyleHeader).Text("Seminar");
                            header.Cell().Element(CellStyleHeader).Text("Laboratore");
                            header.Cell().Element(CellStyleHeader).Text("Praktike");
                            header.Cell().Element(CellStyleHeader).Text("Totale");
                        });
                        foreach (var year in totals.Keys.OrderBy(y => y))
                        {
                            foreach (var semester in totals[year].Keys.OrderBy(s => s))
                            {
                                var t = totals[year][semester];
                                table.Cell().Element(CellStyle).Text(year.ToString());
                                table.Cell().Element(CellStyle).Text(semester.ToString());
                                table.Cell().Element(CellStyle).Text(t.credits.ToString());
                                table.Cell().Element(CellStyle).Text(t.lecture.ToString());
                                table.Cell().Element(CellStyle).Text(t.seminar.ToString());
                                table.Cell().Element(CellStyle).Text(t.lab.ToString());
                                table.Cell().Element(CellStyle).Text(t.practice.ToString());
                                table.Cell().Element(CellStyle).Text(t.total.ToString());
                            }
                        }
                        // Overall row
                        table.Cell().Element(CellStyleHeader).Text("Total");
                        table.Cell().Element(CellStyleHeader).Text("");
                        table.Cell().Element(CellStyleHeader).Text(overall.credits.ToString());
                        table.Cell().Element(CellStyleHeader).Text(overall.lecture.ToString());
                        table.Cell().Element(CellStyleHeader).Text(overall.seminar.ToString());
                        table.Cell().Element(CellStyleHeader).Text(overall.lab.ToString());
                        table.Cell().Element(CellStyleHeader).Text(overall.practice.ToString());
                        table.Cell().Element(CellStyleHeader).Text(overall.total.ToString());
                    })
                );

                // Group and display courses by year/semester
                foreach (var year in courses.Select(c => c.Year).Distinct().OrderBy(y => y))
                {
                    col.Item().Text($"Viti {year}").Bold().FontSize(13).FontColor(Colors.Blue.Medium);
                    foreach (var semester in courses.Where(c => c.Year == year).Select(c => c.Semester).Distinct().OrderBy(s => s))
                    {
                        col.Item().Text($"Semestri {semester}").Bold().FontSize(12).FontColor(Colors.Blue.Darken2);
                        var group = courses.Where(c => c.Year == year && c.Semester == semester).ToList();
                        // Group electives and mandatory
                        var electives = group.Where(c => c.Type == CourseType.Specialized || c.Type == CourseType.Elective || c.Type == CourseType.FinalProject).ToList();
                        var electivesI = electives.Where(c => c.ElectiveGroup == "Elective I").ToList();
                        var electivesII = electives.Where(c => c.ElectiveGroup == "Elective II").ToList();
                        var otherElectives = electives.Where(c => string.IsNullOrEmpty(c.ElectiveGroup)).ToList();
                        var mandatory = group.Where(c => c.Type == CourseType.Mandatory || c.Type == CourseType.Advanced).ToList();
                        // Mandatory courses
                        foreach (var course in mandatory)
                        {
                            col.Item().PaddingTop(10).Element(c => CourseTitleSection(c, course));
                            col.Item().PaddingTop(10).Element(c => TeachingActivityTable(c, course, course.Detail));
                            col.Item().PaddingTop(10).Element(c => EvaluationSection(c, course.Detail));
                            col.Item().PaddingTop(10).Element(c => ObjectivesSection(c, course.Detail));
                            col.Item().PaddingTop(10).Element(c => TopicsSection(c, course.Detail));
                            col.Item().PaddingTop(10).Element(LiteratureSection);
                        }
                        // Electives group
                        if (electives.Any())
                        {
                            col.Item().Text("Lëndë me zgjedhje").Bold().FontSize(12).FontColor(Colors.Purple.Medium);
                            // Elective I
                            if (electivesI.Any())
                            {
                                col.Item().Text("Elective I").Bold().FontSize(11).FontColor(Colors.Blue.Lighten2);
                                foreach (var course in electivesI)
                                {
                                    col.Item().PaddingTop(10).Element(c => CourseTitleSection(c, course));
                                    col.Item().PaddingTop(10).Element(c => TeachingActivityTable(c, course, course.Detail));
                                    col.Item().PaddingTop(10).Element(c => EvaluationSection(c, course.Detail));
                                    col.Item().PaddingTop(10).Element(c => ObjectivesSection(c, course.Detail));
                                    col.Item().PaddingTop(10).Element(c => TopicsSection(c, course.Detail));
                                    col.Item().PaddingTop(10).Element(LiteratureSection);
                                }
                            }
                            // Elective II
                            if (electivesII.Any())
                            {
                                col.Item().Text("Elective II").Bold().FontSize(11).FontColor(Colors.Orange.Lighten2);
                                foreach (var course in electivesII)
                                {
                                    col.Item().PaddingTop(10).Element(c => CourseTitleSection(c, course));
                                    col.Item().PaddingTop(10).Element(c => TeachingActivityTable(c, course, course.Detail));
                                    col.Item().PaddingTop(10).Element(c => EvaluationSection(c, course.Detail));
                                    col.Item().PaddingTop(10).Element(c => ObjectivesSection(c, course.Detail));
                                    col.Item().PaddingTop(10).Element(c => TopicsSection(c, course.Detail));
                                    col.Item().PaddingTop(10).Element(LiteratureSection);
                                }
                            }
                            // Other electives
                            foreach (var course in otherElectives)
                            {
                                col.Item().PaddingTop(10).Element(c => CourseTitleSection(c, course));
                                col.Item().PaddingTop(10).Element(c => TeachingActivityTable(c, course, course.Detail));
                                col.Item().PaddingTop(10).Element(c => EvaluationSection(c, course.Detail));
                                col.Item().PaddingTop(10).Element(c => ObjectivesSection(c, course.Detail));
                                col.Item().PaddingTop(10).Element(c => TopicsSection(c, course.Detail));
                                col.Item().PaddingTop(10).Element(LiteratureSection);
                            }
                        }
                    }
                }
                // Footer (once at the end)
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
