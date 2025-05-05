using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;

namespace SyllabusApplication.Syllabuses.Commands;

public record AddOrRemoveCoursesFromSyllabusCommand(AddOrRemoveCoursesFromSyllabusRequestApiDTO Request)
    : IRequest<ErrorOr<SyllabusResponseApiDTO>>;

public class AddOrRemoveCoursesFromSyllabusCommandHandler
    : IRequestHandler<AddOrRemoveCoursesFromSyllabusCommand, ErrorOr<SyllabusResponseApiDTO>>
{
    private readonly ISyllabusRepository _syllabusRepository;
    private readonly ICourseRepository _courseRepository;

    public AddOrRemoveCoursesFromSyllabusCommandHandler(
        ISyllabusRepository syllabusRepository,
        ICourseRepository courseRepository)
    {
        _syllabusRepository = syllabusRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<SyllabusResponseApiDTO>> Handle(AddOrRemoveCoursesFromSyllabusCommand request, CancellationToken cancellationToken)
    {
        var syllabus = await _syllabusRepository.GetByIdAsync(request.Request.SyllabusId);
        if (syllabus is null)
        {
            return Error.NotFound(description: $"Syllabus with ID {request.Request.SyllabusId} not found.");
        }

        if (request.Request.CourseIdsToRemove.Any())
        {
            var toRemove = syllabus.Courses
                .Where(c => request.Request.CourseIdsToRemove.Contains(c.Id))
                .ToList();

            foreach (var course in toRemove)
            {
                syllabus.Courses.Remove(course);
            }
        }

        if (request.Request.CourseIdsToAdd.Any())
        {
            var coursesToAdd = await _courseRepository.GetByIdsAsync(request.Request.CourseIdsToAdd);

            var alreadyAddedIds = syllabus.Courses.Select(c => c.Id).ToHashSet();
            var filtered = coursesToAdd.Where(c => !alreadyAddedIds.Contains(c.Id)).ToList();

            foreach (var course in filtered)
            {
                syllabus.Courses.Add(course);
            }
        }

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
