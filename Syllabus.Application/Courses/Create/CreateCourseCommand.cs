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

            if (request.Request.Detail is not null)
            {
                course.Detail = new CourseDetail
                {
                    AcademicProgram = request.Request.Detail.AcademicProgram,
                    AcademicYear = request.Request.Detail.AcademicYear,
                    Language = request.Request.Detail.Language,
                    CourseTypeLabel = request.Request.Detail.CourseTypeLabel,
                    EthicsCode = request.Request.Detail.EthicsCode,
                    ExamMethod = request.Request.Detail.ExamMethod,
                    TeachingFormat = request.Request.Detail.TeachingFormat,
                    Credits = request.Request.Detail.Credits,
                    TeachingPlan = request.Request.Detail.TeachingPlan,
                    EvaluationBreakdown = request.Request.Detail.EvaluationBreakdown,
                    Objective = request.Request.Detail.Objective,
                    KeyConcepts = request.Request.Detail.KeyConcepts,
                    Prerequisites = request.Request.Detail.Prerequisites,
                    SkillsAcquired = request.Request.Detail.SkillsAcquired,
                    CourseResponsible = request.Request.Detail.CourseResponsible
                };

                if (request.Request.Detail.Topics is not null)
                {
                    course.Detail.Topics = request.Request.Detail.Topics.Select(t => new Topic
                    {
                        Title = t.Title,
                        Hours = t.Hours,
                        Reference = t.Reference
                    }).ToList();
                }
            }

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
