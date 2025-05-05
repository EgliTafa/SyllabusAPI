using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;

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
                return Error.NotFound(description: $"Syllabus with ID {request.Request.SyllabusId} not found.");
            }

            return new SyllabusResponseApiDTO
            {
                Id = syllabus.Id,
                Name = syllabus.Name,
                Courses = syllabus.Courses.Select(course => new CourseResponseApiDTO
                {
                    Id = course.Id,
                    Title = course.Title,
                    Code = course.Code,
                    Semester = course.Semester,
                    Credits = course.Credits,
                    AcademicProgram = course.Detail?.AcademicProgram,
                    AcademicYear = course.Detail?.AcademicYear,
                    Language = course.Detail?.Language,
                    CourseTypeLabel = course.Detail?.CourseTypeLabel,
                    EthicsCode = course.Detail?.EthicsCode,
                    ExamMethod = course.Detail?.ExamMethod,
                    TeachingFormat = course.Detail?.TeachingFormat,
                    TeachingPlan = course.Detail?.TeachingPlan,
                    EvaluationBreakdown = course.Detail?.EvaluationBreakdown,
                    Objective = course.Detail?.Objective,
                    KeyConcepts = course.Detail?.KeyConcepts,
                    Prerequisites = course.Detail?.Prerequisites,
                    SkillsAcquired = course.Detail?.SkillsAcquired,
                    CourseResponsible = course.Detail?.CourseResponsible,
                    Topics = course.Detail?.Topics?.Select(t => new TopicResponseApiDTO
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
