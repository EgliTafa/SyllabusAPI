using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Syllabus.GetById
{
    public record GetSyllabusByIdQuery(GetSyllabusByIdRequestApiDTO Request) : IRequest<ErrorOr<SyllabusResponseApiDTO>>;

    public class GetSyllabusByIdQueryHandler : IRequestHandler<GetSyllabusByIdQuery, ErrorOr<SyllabusResponseApiDTO>>
    {
        private readonly ISyllabusRepository _syllabusRepository;

        public GetSyllabusByIdQueryHandler(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository;
        }

        public async Task<ErrorOr<SyllabusResponseApiDTO>> Handle(GetSyllabusByIdQuery request, CancellationToken cancellationToken)
        {
            var syllabus = await _syllabusRepository.GetByIdAsync(request.Request.SyllabusId);

            if (syllabus is null)
            {
                return Error.NotFound(description: $"Syllabus with ID {request.Request.SyllabusId} not found.");
            }

            return new SyllabusResponseApiDTO
            {
                Id = syllabus.Id,
                Name = syllabus.Name,
                Courses = syllabus.Courses.Select(course => new CourseResponseApiDTO
                {
                    Id = course.Id,
                    Title = course.Title,
                    Code = course.Code,
                    Semester = course.Semester,
                    Credits = course.Credits,
                    DetailObjective = course.Detail?.Objective,
                    Topics = course.Detail?.Topics?.Select(t => t.Title).ToList() ?? new()
                }).ToList()
            };
        }
    }
}
