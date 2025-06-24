using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Programs;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Syllabus.Create
{
    public record CreateSyllabusCommand(CreateSyllabusRequestApiDTO Request) : IRequest<ErrorOr<SyllabusResponseApiDTO>>;

    public class CreateSyllabusCommandHandler : IRequestHandler<CreateSyllabusCommand, ErrorOr<SyllabusResponseApiDTO>>
    {
        private readonly ISyllabusRepository _syllabusRepository;
        private readonly IProgramRepository _programRepository;

        public CreateSyllabusCommandHandler(
            ISyllabusRepository syllabusRepository,
            IProgramRepository programRepository)
        {
            _syllabusRepository = syllabusRepository ?? throw new ArgumentNullException(nameof(syllabusRepository));
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
        }

        public async Task<ErrorOr<SyllabusResponseApiDTO>> Handle(CreateSyllabusCommand request, CancellationToken cancellationToken)
        {
            // Validate that the program exists
            var program = await _programRepository.GetByIdAsync(request.Request.ProgramId);
            if (program is null)
            {
                return SyllabusErrors.ProgramNotFound;
            }

            var syllabus = new Sylabus
            {
                Name = request.Request.Name,
                AcademicYear = request.Request.AcademicYear,
                ProgramId = request.Request.ProgramId
            };

            var courses = request.Request.Courses.Select(c => new Course
            {
                Title = c.Title,
                Code = c.Code,
                Year = c.Year,
                Semester = c.Semester,
                LectureHours = c.LectureHours,
                SeminarHours = c.SeminarHours,
                LabHours = c.LabHours,
                PracticeHours = c.PracticeHours,
                Credits = c.Credits,
                Evaluation = c.Evaluation,
                Type = c.Type,
                ElectiveGroup = c.ElectiveGroup,
                Detail = c.Detail == null ? null : new CourseDetail
                {
                    AcademicProgram = c.Detail.AcademicProgram,
                    AcademicYear = c.Detail.AcademicYear,
                    Language = c.Detail.Language,
                    CourseTypeLabel = c.Detail.CourseTypeLabel,
                    EthicsCode = c.Detail.EthicsCode,
                    ExamMethod = c.Detail.ExamMethod,
                    TeachingFormat = c.Detail.TeachingFormat,
                    Credits = c.Detail.Credits,
                    TeachingPlan = c.Detail.TeachingPlan,
                    EvaluationBreakdown = c.Detail.EvaluationBreakdown,
                    Objective = c.Detail.Objective,
                    KeyConcepts = c.Detail.KeyConcepts,
                    Prerequisites = c.Detail.Prerequisites,
                    SkillsAcquired = c.Detail.SkillsAcquired,
                    CourseResponsible = c.Detail.CourseResponsible,
                    Topics = c.Detail.Topics?.Select(t => new Topic
                    {
                        Title = t.Title,
                        Hours = t.Hours,
                        Reference = t.Reference
                    }).ToList()
                }
            }).ToList();

            foreach (var course in courses)
            {
                syllabus.Courses.Add(course);
            }

            await _syllabusRepository.AddAsync(syllabus);
            await _syllabusRepository.SaveChangesAsync();

            return new SyllabusResponseApiDTO
            {
                Id = syllabus.Id,
                Name = syllabus.Name,
                AcademicYear = syllabus.AcademicYear,
                Program = new ProgramResponseApiDTO
                {
                    Id = program.Id,
                    Name = program.Name,
                    Description = program.Description,
                    DepartmentId = program.DepartmentId,
                    DepartmentName = program.Department.Name,
                    CreatedAt = program.CreatedAt,
                    UpdatedAt = program.UpdatedAt
                },
                Courses = syllabus.Courses.Select(c => new CourseResponseApiDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Code = c.Code,
                    Year = c.Year,
                    Semester = c.Semester,
                    Credits = c.Credits,
                    LectureHours = c.LectureHours,
                    SeminarHours = c.SeminarHours,
                    LabHours = c.LabHours,
                    PracticeHours = c.PracticeHours,
                    ElectiveGroup = c.ElectiveGroup,
                    AcademicProgram = c.Detail?.AcademicProgram,
                    AcademicYear = c.Detail?.AcademicYear,
                    Language = c.Detail?.Language,
                    CourseTypeLabel = c.Detail?.CourseTypeLabel,
                    EthicsCode = c.Detail?.EthicsCode,
                    ExamMethod = c.Detail?.ExamMethod,
                    TeachingFormat = c.Detail?.TeachingFormat,
                    TeachingPlan = c.Detail?.TeachingPlan,
                    EvaluationBreakdown = c.Detail?.EvaluationBreakdown,
                    Objective = c.Detail?.Objective,
                    KeyConcepts = c.Detail?.KeyConcepts,
                    Prerequisites = c.Detail?.Prerequisites,
                    SkillsAcquired = c.Detail?.SkillsAcquired,
                    CourseResponsible = c.Detail?.CourseResponsible,
                    Topics = c.Detail?.Topics?.Select(t => new TopicResponseApiDTO
                    {
                        Title = t.Title,
                        Hours = t.Hours,
                        Reference = t.Reference
                    }).ToList()
                }).ToList()
            };
        }
    }
}
