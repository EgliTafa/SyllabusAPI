using ErrorOr;
using MediatR;
using Microsoft.Extensions.Options;
using QuestPDF.Fluent;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;
using Syllabus.Util.Options;

namespace Syllabus.Application.Courses.Export
{
    public record ExportCoursePdfCommand(int CourseId) : IRequest<ErrorOr<ExportSyllabusPdfResponseApiDTO>>;

    public class ExportCoursePdfCommandHandler : IRequestHandler<ExportCoursePdfCommand, ErrorOr<ExportSyllabusPdfResponseApiDTO>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly StaticFilesOptions _staticFiles;

        public ExportCoursePdfCommandHandler(ICourseRepository courseRepository, IOptions<StaticFilesOptions> staticFilesOptions)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _staticFiles = staticFilesOptions.Value;
        }

        public async Task<ErrorOr<ExportSyllabusPdfResponseApiDTO>> Handle(ExportCoursePdfCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course is null)
                return CourseErrors.CourseNotFound;

            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), _staticFiles.RootPath, "logo.png");
            var document = new CoursePdfDocument(course, logoPath);
            byte[] pdfBytes = document.GeneratePdf();

            return new ExportSyllabusPdfResponseApiDTO
            {
                FileName = $"{course.Title}.pdf",
                ContentType = "application/pdf",
                FileBytes = pdfBytes
            };
        }
    }
} 