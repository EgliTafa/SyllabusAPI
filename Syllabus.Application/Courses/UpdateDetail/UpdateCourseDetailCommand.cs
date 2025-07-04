using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Courses.UpdateDetail
{
    public record UpdateCourseDetailCommand(int CourseId, CourseDetailRequestApiDTO Request) : IRequest<ErrorOr<CourseResponseApiDTO>>;

    public class UpdateCourseDetailCommandHandler : IRequestHandler<UpdateCourseDetailCommand, ErrorOr<CourseResponseApiDTO>>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseDetailCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ErrorOr<CourseResponseApiDTO>> Handle(UpdateCourseDetailCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId);

            if (course is null)
            {
                return CourseErrors.CourseNotFound;
            }

            if (course.Detail is null)
            {
                return CourseErrors.CourseDetailNotFound;
            }

            course.Detail.AcademicProgram = request.Request.AcademicProgram;
            course.Detail.AcademicYear = request.Request.AcademicYear;
            course.Detail.Language = request.Request.Language;
            course.Detail.CourseTypeLabel = request.Request.CourseTypeLabel;
            course.Detail.EthicsCode = request.Request.EthicsCode;
            course.Detail.ExamMethod = request.Request.ExamMethod;
            course.Detail.TeachingFormat = request.Request.TeachingFormat;
            course.Detail.Credits = request.Request.Credits;
            course.Detail.TeachingPlan = request.Request.TeachingPlan;
            course.Detail.EvaluationBreakdown = request.Request.EvaluationBreakdown;
            course.Detail.Objective = request.Request.Objective;
            course.Detail.KeyConcepts = request.Request.KeyConcepts;
            course.Detail.Prerequisites = request.Request.Prerequisites;
            course.Detail.SkillsAcquired = request.Request.SkillsAcquired;
            course.Detail.CourseResponsible = request.Request.CourseResponsible;

            if (request.Request.Topics is not null)
            {
                course.Detail.Topics = request.Request.Topics.Select(t => new Topic
                {
                    Title = t.Title,
                    Hours = t.Hours,
                    Reference = t.Reference
                }).ToList();
            }

            await _courseRepository.SaveChangesAsync();

            return new CourseResponseApiDTO
            {
                Id = course.Id,
                Title = course.Title,
                Code = course.Code,
                Semester = course.Semester,
                Credits = course.Credits,
                Objective = course.Detail.Objective,
                Topics = course.Detail.Topics?.Select(t => new TopicResponseApiDTO
                {
                    Title = t.Title,
                    Hours = t.Hours,
                    Reference = t.Reference
                }).ToList()
            };
        }
    }
} 