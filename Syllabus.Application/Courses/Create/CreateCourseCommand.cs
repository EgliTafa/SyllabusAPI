using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Courses.Create
{
    public record CreateCourseCommand(CreateCourseRequestApiDTO Request) : IRequest<ErrorOr<CourseResponseApiDTO>>;

    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<CourseResponseApiDTO>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISyllabusRepository _syllabusRepository;

        public CreateCourseCommandHandler(ICourseRepository courseRepository, ISyllabusRepository syllabusRepository)
        {
            _courseRepository = courseRepository;
            _syllabusRepository = syllabusRepository;
        }

        public async Task<ErrorOr<CourseResponseApiDTO>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var syllabus = await _syllabusRepository.GetByIdAsync(request.Request.SyllabusId);

            if (syllabus is null)
            {
                return Error.NotFound(description: $"Syllabus with ID {request.Request.SyllabusId} not found.");
            }

            var course = new Course
            {
                Title = request.Request.Title,
                Code = request.Request.Code,
                Semester = request.Request.Semester,
                LectureHours = request.Request.LectureHours,
                SeminarHours = request.Request.SeminarHours,
                LabHours = request.Request.LabHours,
                Credits = request.Request.Credits,
                Evaluation = request.Request.Evaluation,
                Type = request.Request.Type
            };

            syllabus.Courses.Add(course);
            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();

            return new CourseResponseApiDTO
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Semester = course.Semester,
                Credits = course.Credits,
                Topics = new(),
                DetailObjective = null
            };
        }
    }
}
