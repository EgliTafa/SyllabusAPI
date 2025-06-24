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
                AcademicYear = syllabus.AcademicYear,
                Program = new ProgramResponseApiDTO
                {
                    Id = syllabus.Program.Id,
                    Name = syllabus.Program.Name,
                    Description = syllabus.Program.Description,
                    DepartmentId = syllabus.Program.DepartmentId,
                    DepartmentName = syllabus.Program.Department.Name,
                    CreatedAt = syllabus.Program.CreatedAt,
                    UpdatedAt = syllabus.Program.UpdatedAt
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
