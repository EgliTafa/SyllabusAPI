using ErrorOr;
using MediatR;
using Microsoft.Extensions.Options;
using QuestPDF.Fluent;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;
using Syllabus.Util.Options;

namespace Syllabus.Application.Syllabus.Export
{
    public record ExportSyllabusPdfCommand(ExportSyllabusPdfRequestApiDTO Request) : IRequest<ErrorOr<ExportSyllabusPdfResponseApiDTO>>;

    public class ExportSyllabusPdfCommandHandler : IRequestHandler<ExportSyllabusPdfCommand, ErrorOr<ExportSyllabusPdfResponseApiDTO>>
    {
        private readonly ISyllabusRepository _syllabusRepository;
        private readonly StaticFilesOptions _staticFiles;


        public ExportSyllabusPdfCommandHandler(ISyllabusRepository syllabusRepository, IOptions<StaticFilesOptions> staticFilesOptions)
        {
            _syllabusRepository = syllabusRepository ?? throw new ArgumentNullException(nameof(syllabusRepository));
            _staticFiles = staticFilesOptions.Value;
        }

        public async Task<ErrorOr<ExportSyllabusPdfResponseApiDTO>> Handle(ExportSyllabusPdfCommand request, CancellationToken cancellationToken)
        {
            Sylabus? syllabus = await _syllabusRepository.GetByIdAsync(request.Request.SyllabusId);
            if (syllabus is null)
                return Error.NotFound("Syllabus.NotFound", "Syllabus with the given ID does not exist.");

            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), _staticFiles.RootPath, "logo.png");
            SyllabusPdfDocument document = new SyllabusPdfDocument(syllabus, logoPath);

            byte[] pdfBytes = document.GeneratePdf();

            return new ExportSyllabusPdfResponseApiDTO
            {
                FileName = $"{syllabus.Name}.pdf",
                ContentType = "application/pdf",
                FileBytes = pdfBytes
            };
        }
    }
}
