using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Courses.Delete
{
    public record DeleteCourseCommand(DeleteCourseRequestApiDTO Request) : IRequest<ErrorOr<Deleted>>;

    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, ErrorOr<Deleted>>
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Request.CourseId);

            if (course is null)
            {
                return Error.NotFound(description: $"Course with ID {request.Request.CourseId} not found.");
            }

            _courseRepository.Remove(course);
            await _courseRepository.SaveChangesAsync();

            return Result.Deleted;
        }
    }
}
