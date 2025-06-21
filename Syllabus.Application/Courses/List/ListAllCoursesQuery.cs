using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace SyllabusApplication.Courses.Queries;

public record ListAllCoursesQuery(ListAllCoursesRequestApiDTO Request) : IRequest<ErrorOr<ListAllCoursesResponseApiDTO>>;

public class ListAllCoursesQueryHandler : IRequestHandler<ListAllCoursesQuery, ErrorOr<ListAllCoursesResponseApiDTO>>
{
    private readonly ICourseRepository _courseRepository;

    public ListAllCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
    }

    public async Task<ErrorOr<ListAllCoursesResponseApiDTO>> Handle(ListAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllAsync();

        var dto = new ListAllCoursesResponseApiDTO
        {
            AllCourses = courses?.Select(course => new CourseResponseApiDTO
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Semester = course.Semester,
                Credits = course.Credits,
                Year = course.Year,
                LectureHours = course.LectureHours,
                SeminarHours = course.SeminarHours,
                LabHours = course.LabHours,
                PracticeHours = course.PracticeHours,
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
                }).ToList(),
                ElectiveGroup = course.ElectiveGroup
            }).ToList() ?? new List<CourseResponseApiDTO>()
        };

        return dto;
    }
}
