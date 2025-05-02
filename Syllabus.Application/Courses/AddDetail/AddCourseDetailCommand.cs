using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Courses.AddDetail
{
    public record AddCourseDetailCommand(int CourseId, CourseDetailRequestApiDTO Request) : IRequest<ErrorOr<CourseResponseApiDTO>>;

    public class AddCourseDetailCommandHandler : IRequestHandler<AddCourseDetailCommand, ErrorOr<CourseResponseApiDTO>>
    {
        private readonly ICourseRepository _courseRepository;

        public AddCourseDetailCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ErrorOr<CourseResponseApiDTO>> Handle(AddCourseDetailCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);

            if (course is null)
            {
                return Error.NotFound(description: $"Course with ID {request.CourseId} not found.");
            }

            if (course.Detail is not null)
            {
                return Error.Conflict(description: $"Course with ID {request.CourseId} already has details.");
            }

            var courseDetail = new CourseDetail
            {
                CourseId = request.CourseId,
                AcademicProgram = request.Request.AcademicProgram,
                AcademicYear = request.Request.AcademicYear,
                Language = request.Request.Language,
                CourseTypeLabel = request.Request.CourseTypeLabel,
                EthicsCode = request.Request.EthicsCode,
                ExamMethod = request.Request.ExamMethod,
                TeachingFormat = request.Request.TeachingFormat,
                Credits = request.Request.Credits,
                TeachingPlan = request.Request.TeachingPlan,
                EvaluationBreakdown = request.Request.EvaluationBreakdown,
                Objective = request.Request.Objective,
                KeyConcepts = request.Request.KeyConcepts,
                Prerequisites = request.Request.Prerequisites,
                SkillsAcquired = request.Request.SkillsAcquired,
                CourseResponsible = request.Request.CourseResponsible
            };

            if (request.Request.Topics is not null)
            {
                courseDetail.Topics = request.Request.Topics.Select(t => new Topic
                {
                    Title = t.Title,
                    Hours = t.Hours,
                    Reference = t.Reference
                }).ToList();
            }

            course.Detail = courseDetail;
            await _courseRepository.AddDetailAsync(courseDetail);
            await _courseRepository.SaveChangesAsync();

            return new CourseResponseApiDTO
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Semester = course.Semester,
                Credits = course.Credits,
                DetailObjective = course.Detail.Objective,
                Topics = course.Detail.Topics?.Select(t => t.Title).ToList() ?? new()
            };
        }
    }
}