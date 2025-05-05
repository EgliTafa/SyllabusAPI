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
        await _syllabusRepository.SaveChangesAsync();

        return new SyllabusResponseApiDTO
        {
            Id = syllabus.Id,
            Name = syllabus.Name,
            Courses = syllabus.Courses.Select(c => new CourseResponseApiDTO
            {
                Id = c.Id,
                Title = c.Title,
                Code = c.Code,
                Semester = c.Semester,
                Credits = c.Credits,
                Objective = c.Detail?.Objective,
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
