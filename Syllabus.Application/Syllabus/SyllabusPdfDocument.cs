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

                // Main Table (matches the provided image)
                col.Item().PaddingTop(10).Element(MainSyllabusTableSection);

                // Elective Courses Section
                col.Item().PaddingTop(20).Element(ElectiveCoursesSection);

                // Footer (once at the end)
                col.Item().PaddingTop(20).Element(FooterSection);

                // Notes section at the bottom
                col.Item().PaddingTop(10).Element(NotesSection);
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

        private void MainSyllabusTableSection(IContainer container)
        {
            var courses = _syllabus.Courses.OrderBy(c => c.Year).ThenBy(c => c.Semester).ThenBy(c => c.Id).ToList();
            var years = courses.Select(c => c.Year).Distinct().OrderBy(y => y).ToList();
            var semesters = new[] { 1, 2, 3, 4, 5, 6 };
            var electives = courses.Where(c => c.Type == CourseType.Specialized || c.Type == CourseType.Elective || c.Type == CourseType.FinalProject).ToList();
            var mandatory = courses.Except(electives).ToList();

            // Precompute rowspans for years and semesters
            var yearSemesterGroups = years.ToDictionary(
                year => year,
                year => semesters.ToDictionary(
                    sem => sem,
                    sem => mandatory.Count(c => c.Year == year && c.Semester == sem)
                )
            );
            var yearRowspans = years.ToDictionary(
                year => year,
                year => yearSemesterGroups[year].Values.Sum()
            );

            // Precompute summary rows
            var yearSummaries = years.ToDictionary(
                year => year,
                year => mandatory.Where(c => c.Year == year)
            );

            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(24); // Nr.
                    columns.RelativeColumn(3); // Lënda
                    columns.ConstantColumn(28); // Tipi
                    columns.ConstantColumn(28); // Viti
                    columns.ConstantColumn(36); // Semestri
                    columns.ConstantColumn(24); // Le
                    columns.ConstantColumn(24); // Se
                    columns.ConstantColumn(24); // La
                    columns.ConstantColumn(24); // Pr
                    columns.ConstantColumn(36); // Totali
                    columns.ConstantColumn(36); // Praktikë
                    columns.ConstantColumn(36); // Totali orë
                    columns.ConstantColumn(36); // Kredite
                    columns.ConstantColumn(36); // Mënyra
                });

                table.Header(header =>
                {
                    header.Cell().ColumnSpan(14).Element(CellStyleHeader).Text($"PROGRAMI I STUDIMIT {(_syllabus.Program?.Name ?? "")}").FontSize(12).Bold().AlignCenter();
                    header.Cell().ColumnSpan(14).Element(CellStyleHeader).Text($"VITI AKADEMIK {(_syllabus.Program?.AcademicYear ?? "")}").FontSize(11).Bold().AlignCenter();
                    header.Cell().ColumnSpan(14).Element(CellStyleHeader).Text("");

                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Nr.");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Lënda");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Tipi");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Viti");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Semestri");
                    header.Cell().ColumnSpan(4).Element(CellStyleHeader).Text("");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Totali");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Praktikë");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Totali orë");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Kredite");
                    header.Cell().RowSpan(3).Element(CellStyleHeader).Text("Mënyra");

                    header.Cell().ColumnSpan(4).Element(CellStyleHeader).Text("Semestri");
                    for (int i = 0; i < 9; i++) header.Cell().Element(CellStyleHeader).Text("");

                    header.Cell().Element(CellStyleHeader).Text("Le");
                    header.Cell().Element(CellStyleHeader).Text("Se");
                    header.Cell().Element(CellStyleHeader).Text("La");
                    header.Cell().Element(CellStyleHeader).Text("Pr");
                    for (int i = 0; i < 9; i++) header.Cell().Element(CellStyleHeader).Text("");
                });

                int nr = 1;
                foreach (var year in years)
                {
                    bool yearFirstRow = true;
                    foreach (var semester in semesters)
                    {
                        var group = mandatory.Where(c => c.Year == year && c.Semester == semester).ToList();
                        if (group.Count == 0) continue;
                        bool semesterFirstRow = true;
                        foreach (var course in group)
                        {
                            if (yearFirstRow)
                            {
                                table.Cell().RowSpan((uint)yearRowspans[year]).Element(CellStyleHeader).Text($"VITI {year}").Bold();
                                yearFirstRow = false;
                            }
                            if (semesterFirstRow)
                            {
                                table.Cell().RowSpan((uint)group.Count).Element(CellStyleHeader).Text($"Semestri {semester}").Bold();
                                semesterFirstRow = false;
                            }
                            table.Cell().Element(CellStyle).Text(nr.ToString());
                            table.Cell().Element(CellStyle).AlignLeft().Text(course.Title);
                            table.Cell().Element(CellStyle).Text(course.Detail?.CourseTypeLabel ?? course.Type.ToString());
                            table.Cell().Element(CellStyle).Text(year.ToString());
                            table.Cell().Element(CellStyle).Text(semester.ToString());
                    table.Cell().Element(CellStyle).Text(course.LectureHours.ToString());
                    table.Cell().Element(CellStyle).Text(course.SeminarHours.ToString());
                    table.Cell().Element(CellStyle).Text(course.LabHours.ToString());
                            table.Cell().Element(CellStyle).Text((course.Detail?.TeachingPlan?.PracticeHours ?? 0).ToString());
                            int total = course.LectureHours + course.SeminarHours + course.LabHours + (course.Detail?.TeachingPlan?.PracticeHours ?? 0);
                            table.Cell().Element(CellStyle).Text(total.ToString());
                            table.Cell().Element(CellStyle).Text((course.Detail?.TeachingPlan?.PracticeHours ?? 0).ToString());
                    table.Cell().Element(CellStyle).Text(total.ToString());
                            table.Cell().Element(CellStyle).Text(course.Credits.ToString());
                            table.Cell().Element(CellStyle).Text(course.Detail?.ExamMethod ?? course.Evaluation.ToString());
                            nr++;
                        }
                    }
                    var yearCourses = yearSummaries[year].ToList();
                    int yearLe = yearCourses.Sum(c => c.LectureHours);
                    int yearSe = yearCourses.Sum(c => c.SeminarHours);
                    int yearLa = yearCourses.Sum(c => c.LabHours);
                    int yearPr = yearCourses.Sum(c => c.Detail?.TeachingPlan?.PracticeHours ?? 0);
                    int yearTot = yearCourses.Sum(c => c.LectureHours + c.SeminarHours + c.LabHours + (c.Detail?.TeachingPlan?.PracticeHours ?? 0));
                    int yearKredite = yearCourses.Sum(c => c.Credits);
                    table.Cell().ColumnSpan(5).Element(CellStyleSummary).Text($"Gjithsej Viti {year} orë/kredite:", TextStyle.Default.Bold());
                    table.Cell().Element(CellStyleSummary).Text(x => x.Span(yearLe.ToString()).Bold());
                    table.Cell().Element(CellStyleSummary).Text(x => x.Span(yearSe.ToString()).Bold());
                    table.Cell().Element(CellStyleSummary).Text(x => x.Span(yearLa.ToString()).Bold());
                    table.Cell().Element(CellStyleSummary).Text(x => x.Span(yearPr.ToString()).Bold());
                    table.Cell().Element(CellStyleSummary).Text(x => x.Span(yearTot.ToString()).Bold());
                    table.Cell().Element(CellStyleSummary).Text(x => x.Span(yearKredite.ToString()).Bold());
                    table.Cell().Element(CellStyleSummary).Text("");
                }
                int totalLe = mandatory.Sum(c => c.LectureHours);
                int totalSe = mandatory.Sum(c => c.SeminarHours);
                int totalLa = mandatory.Sum(c => c.LabHours);
                int totalPr = mandatory.Sum(c => c.Detail?.TeachingPlan?.PracticeHours ?? 0);
                int totalTot = mandatory.Sum(c => c.LectureHours + c.SeminarHours + c.LabHours + (c.Detail?.TeachingPlan?.PracticeHours ?? 0));
                int totalKredite = mandatory.Sum(c => c.Credits);
                table.Cell().ColumnSpan(5).Element(CellStyleSummary).Text("TOTALI", TextStyle.Default.Bold());
                table.Cell().Element(CellStyleSummary).Text(x => x.Span(totalLe.ToString()).Bold());
                table.Cell().Element(CellStyleSummary).Text(x => x.Span(totalSe.ToString()).Bold());
                table.Cell().Element(CellStyleSummary).Text(x => x.Span(totalLa.ToString()).Bold());
                table.Cell().Element(CellStyleSummary).Text(x => x.Span(totalPr.ToString()).Bold());
                table.Cell().Element(CellStyleSummary).Text(x => x.Span(totalTot.ToString()).Bold());
                table.Cell().Element(CellStyleSummary).Text(x => x.Span(totalKredite.ToString()).Bold());
                table.Cell().Element(CellStyleSummary).Text("");
            });
        }

        private void ElectiveCoursesSection(IContainer container)
        {
            var courses = _syllabus.Courses.OrderBy(c => c.Year).ThenBy(c => c.Semester).ThenBy(c => c.Id).ToList();
            var electives = courses.Where(c => c.Type == CourseType.Specialized || c.Type == CourseType.Elective || c.Type == CourseType.FinalProject).ToList();
            if (!electives.Any())
            {
                container.Text("");
                return;
            }
            container.Column(col =>
            {
                col.Item().Text("Lëndë me zgjedhje").Bold().FontSize(12).FontColor(Colors.Purple.Medium);
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(24); // Nr.
                        columns.RelativeColumn(3); // Lënda
                        columns.ConstantColumn(28); // Tipi
                        columns.ConstantColumn(28); // Viti
                        columns.ConstantColumn(36); // Semestri
                        columns.ConstantColumn(24); // Le
                        columns.ConstantColumn(24); // Se
                        columns.ConstantColumn(24); // La
                        columns.ConstantColumn(24); // Pr
                        columns.ConstantColumn(36); // Totali
                        columns.ConstantColumn(36); // Praktikë
                        columns.ConstantColumn(36); // Totali orë
                        columns.ConstantColumn(36); // Kredite
                        columns.ConstantColumn(36); // Mënyra
                        });
                        table.Header(header =>
                        {
                        header.Cell().Element(CellStyleHeader).Text("Nr.");
                        header.Cell().Element(CellStyleHeader).Text("Lënda");
                        header.Cell().Element(CellStyleHeader).Text("Tipi");
                        header.Cell().Element(CellStyleHeader).Text("Viti");
                        header.Cell().Element(CellStyleHeader).Text("Semestri");
                        header.Cell().Element(CellStyleHeader).Text("Le");
                        header.Cell().Element(CellStyleHeader).Text("Se");
                        header.Cell().Element(CellStyleHeader).Text("La");
                        header.Cell().Element(CellStyleHeader).Text("Pr");
                        header.Cell().Element(CellStyleHeader).Text("Totali");
                        header.Cell().Element(CellStyleHeader).Text("Praktikë");
                        header.Cell().Element(CellStyleHeader).Text("Totali orë");
                        header.Cell().Element(CellStyleHeader).Text("Kredite");
                        header.Cell().Element(CellStyleHeader).Text("Mënyra");
                    });
                    int nr = 1;
                    foreach (var course in electives)
                    {
                        table.Cell().Element(CellStyle).Text(nr.ToString());
                        table.Cell().Element(CellStyle).AlignLeft().Text(course.Title);
                        table.Cell().Element(CellStyle).Text(course.Detail?.CourseTypeLabel ?? course.Type.ToString());
                        table.Cell().Element(CellStyle).Text(course.Year.ToString());
                        table.Cell().Element(CellStyle).Text(course.Semester.ToString());
                        table.Cell().Element(CellStyle).Text(course.LectureHours.ToString());
                        table.Cell().Element(CellStyle).Text(course.SeminarHours.ToString());
                        table.Cell().Element(CellStyle).Text(course.LabHours.ToString());
                        table.Cell().Element(CellStyle).Text((course.Detail?.TeachingPlan?.PracticeHours ?? 0).ToString());
                        int total = course.LectureHours + course.SeminarHours + course.LabHours + (course.Detail?.TeachingPlan?.PracticeHours ?? 0);
                        table.Cell().Element(CellStyle).Text(total.ToString());
                        table.Cell().Element(CellStyle).Text((course.Detail?.TeachingPlan?.PracticeHours ?? 0).ToString());
                        table.Cell().Element(CellStyle).Text(total.ToString());
                        table.Cell().Element(CellStyle).Text(course.Credits.ToString());
                        table.Cell().Element(CellStyle).Text(course.Detail?.ExamMethod ?? course.Evaluation.ToString());
                        nr++;
                    }
                });
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

        private void NotesSection(IContainer container)
        {
            container.Text("Shënim: P=Provim; V=Vlerësim i vazhduar; F=Fiton; Semestri VI ka 14 Javë").FontSize(9).Italic().FontColor(Colors.Grey.Darken2).AlignLeft();
        }

        private IContainer CellStyle(IContainer container) =>
            container.Border(0.5f).Padding(2).AlignCenter().AlignMiddle();
        private IContainer CellStyleHeader(IContainer container) =>
            container.Background(Colors.Grey.Lighten3).Border(0.5f).Padding(2).AlignCenter().AlignMiddle();
        private IContainer CellStyleSummary(IContainer container) =>
            container.Background(Colors.Orange.Lighten4).Border(0.5f).Padding(2).AlignCenter().AlignMiddle();
    }
}
