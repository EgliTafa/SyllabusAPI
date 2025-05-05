using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Courses.Update
{
    public record UpdateCourseCommand(UpdateCourseRequestApiDTO Request) : IRequest<ErrorOr<CourseResponseApiDTO>>;

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ErrorOr<CourseResponseApiDTO>>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ErrorOr<CourseResponseApiDTO>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Request.CourseId);

            if (course is null)
            {
                return Error.NotFound(description: $"Course with ID {request.Request.CourseId} not found.");
            }

            course.Title = request.Request.Title;
            course.Code = request.Request.Code;
            course.Semester = request.Request.Semester;
            course.LectureHours = request.Request.LectureHours;
            course.SeminarHours = request.Request.SeminarHours;
            course.LabHours = request.Request.LabHours;
            course.Credits = request.Request.Credits;
            course.Evaluation = request.Request.Evaluation;
            course.Type = request.Request.Type;

            await _courseRepository.SaveChangesAsync();

            return new CourseResponseApiDTO
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
            };
        }
    }

}
