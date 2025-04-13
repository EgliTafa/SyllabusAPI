using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Courses.GetById
{
    public record GetCourseByIdQuery(GetCourseByIdRequest Request) : IRequest<ErrorOr<CourseResponseApiDTO>>;

    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, ErrorOr<CourseResponseApiDTO>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        }

        public async Task<ErrorOr<CourseResponseApiDTO>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Request.CourseId);

            if (course is null)
            {
                return Error.NotFound(description: $"Course with ID {request.Request.CourseId} not found.");
            }

            return new CourseResponseApiDTO
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Semester = course.Semester,
                Credits = course.Credits,
                DetailObjective = course.Detail?.Objective,
                Topics = course.Detail?.Topics?.Select(t => t.Title).ToList() ?? new()
            };
        }
    }
}
