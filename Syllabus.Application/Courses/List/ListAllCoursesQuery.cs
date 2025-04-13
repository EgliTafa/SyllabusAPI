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

        if (courses is null || courses.Count == 0)
        {
            return Error.NotFound(description: "No courses found.");
        }

        var dto = new ListAllCoursesResponseApiDTO
        {
            AllCourses = courses.Select(course => new CourseResponseApiDTO
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Semester = course.Semester,
                Credits = course.Credits,
                DetailObjective = course.Detail?.Objective,
                Topics = course.Detail?.Topics?.Select(t => t.Title).ToList() ?? new()
            }).ToList()
        };

        return dto;
    }
}
