using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Syllabus.List
{
    public record ListAllSyllabusesQuery(ListAllSyllabusesRequestApiDTO Request) : IRequest<ErrorOr<ListAllSyllabusesResponseApiDTO>>;

    public class ListAllSyllabusesQueryHandler : IRequestHandler<ListAllSyllabusesQuery, ErrorOr<ListAllSyllabusesResponseApiDTO>>
    {
        private readonly ISyllabusRepository _syllabusRepository;

        public ListAllSyllabusesQueryHandler(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository ?? throw new ArgumentNullException(nameof(syllabusRepository));
        }

        public async Task<ErrorOr<ListAllSyllabusesResponseApiDTO>> Handle(ListAllSyllabusesQuery request, CancellationToken cancellationToken)
        {
            var syllabuses = await _syllabusRepository.GetAllAsync();

            if (syllabuses == null || syllabuses.Count == 0)
            {
                return SyllabusErrors.SyllabusNotFound;
            }

            return new ListAllSyllabusesResponseApiDTO
            {
                Syllabuses = syllabuses.Select(s => new SyllabusResponseApiDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    AcademicYear = s.AcademicYear,
                    Courses = s.Courses.Select(c => new CourseResponseApiDTO
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
                }).ToList()
            };
        }
    }
}
