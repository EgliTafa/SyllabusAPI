using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;

namespace SyllabusApplication.Syllabuses.Commands;

public record UpdateSyllabusCommand(UpdateSyllabusRequestApiDTO Request) : IRequest<ErrorOr<SyllabusResponseApiDTO>>;

public class UpdateSyllabusCommandHandler : IRequestHandler<UpdateSyllabusCommand, ErrorOr<SyllabusResponseApiDTO>>
{
    private readonly ISyllabusRepository _syllabusRepository;

    public UpdateSyllabusCommandHandler(ISyllabusRepository syllabusRepository)
    {
        _syllabusRepository = syllabusRepository;
    }

    public async Task<ErrorOr<SyllabusResponseApiDTO>> Handle(UpdateSyllabusCommand request, CancellationToken cancellationToken)
    {
        var syllabus = await _syllabusRepository.GetByIdAsync(request.Request.SyllabusId);
        if (syllabus is null)
        {
            return Error.NotFound(description: $"Syllabus with ID {request.Request.SyllabusId} not found.");
        }

        syllabus.Name = request.Request.Name;
        syllabus.AcademicYear = request.Request.AcademicYear;
        await _syllabusRepository.SaveChangesAsync();

        return new SyllabusResponseApiDTO
        {
            Id = syllabus.Id,
            Name = syllabus.Name,
            AcademicYear = syllabus.AcademicYear,
            Courses = syllabus.Courses.Select(c => new CourseResponseApiDTO
            {
                Id = c.Id,
                Title = c.Title,
                Code = c.Code,
                Year = c.Year,
                Semester = c.Semester,
                Credits = c.Credits,
                LectureHours = c.LectureHours,
                SeminarHours = c.SeminarHours,
                LabHours = c.LabHours,
                PracticeHours = c.PracticeHours,
                ElectiveGroup = c.ElectiveGroup,
                AcademicProgram = c.Detail?.AcademicProgram,
                AcademicYear = c.Detail?.AcademicYear,
                Language = c.Detail?.Language,
                CourseTypeLabel = c.Detail?.CourseTypeLabel,
                EthicsCode = c.Detail?.EthicsCode,
                ExamMethod = c.Detail?.ExamMethod,
                TeachingFormat = c.Detail?.TeachingFormat,
                TeachingPlan = c.Detail?.TeachingPlan,
                EvaluationBreakdown = c.Detail?.EvaluationBreakdown,
                Objective = c.Detail?.Objective,
                KeyConcepts = c.Detail?.KeyConcepts,
                Prerequisites = c.Detail?.Prerequisites,
                SkillsAcquired = c.Detail?.SkillsAcquired,
                CourseResponsible = c.Detail?.CourseResponsible,
                Topics = c.Detail?.Topics?.Select(t => new TopicResponseApiDTO
                {
                    Title = t.Title,
                    Hours = t.Hours,
                    Reference = t.Reference
                }).ToList()
            }).ToList()
        };
    }
}
