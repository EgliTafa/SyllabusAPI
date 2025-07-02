using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Programs;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;
using SyllabusErrors = Syllabus.Application.Syllabus.SyllabusErrors;

namespace Syllabus.Application.Syllabus.GetById
{
    public record GetSyllabusByIdQuery(GetSyllabusByIdRequestApiDTO Request) : IRequest<ErrorOr<SyllabusResponseApiDTO>>;

    public class GetSyllabusByIdQueryHandler : IRequestHandler<GetSyllabusByIdQuery, ErrorOr<SyllabusResponseApiDTO>>
    {
        private readonly ISyllabusRepository _syllabusRepository;

        public GetSyllabusByIdQueryHandler(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository;
        }

        public async Task<ErrorOr<SyllabusResponseApiDTO>> Handle(GetSyllabusByIdQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _syllabusRepository.GetByIdAsync(request.Request.SyllabusId);

            if (syllabus is null)
            {
                return SyllabusErrors.SyllabusNotFound;
            }

            return new SyllabusResponseApiDTO
            {
                Id = syllabus.Id,
                Name = syllabus.Name,
                Program = new ProgramResponseApiDTO
                {
                    Id = syllabus.ProgramAcademicYear.Program.Id,
                    Name = syllabus.ProgramAcademicYear.Program.Name,
                    Description = syllabus.ProgramAcademicYear.Program.Description,
                    DepartmentId = syllabus.ProgramAcademicYear.Program.DepartmentId,
                    DepartmentName = syllabus.ProgramAcademicYear.Program.Department.Name,
                    CreatedAt = syllabus.ProgramAcademicYear.Program.CreatedAt,
                    UpdatedAt = syllabus.ProgramAcademicYear.Program.UpdatedAt,
                    AcademicYears = syllabus.ProgramAcademicYear.Program.AcademicYears.Select(ay => new ProgramAcademicYearDTO
                    {
                        Id = ay.Id,
                        AcademicYear = ay.AcademicYear
                    }).ToList()
                },
                ProgramAcademicYear = new ProgramAcademicYearDTO
                {
                    Id = syllabus.ProgramAcademicYear.Id,
                    AcademicYear = syllabus.ProgramAcademicYear.AcademicYear
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
