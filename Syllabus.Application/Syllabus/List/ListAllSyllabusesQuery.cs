using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Courses;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Syllabus.List
{
    public record ListAllSyllabusesQuery(ListAllSyllabusesRequestApiDTO Request) : IRequest<ErrorOr<ListAllSyllabusesResponseApiDTO>>;

    public class ListAllSyllabusesQueryHandler : IRequestHandler<ListAllSyllabusesQuery, ErrorOr<ListAllSyllabusesResponseApiDTO>>
    {
        private readonly ISyllabusRepository _syllabusRepository;

        public ListAllSyllabusesQueryHandler(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository ?? throw new ArgumentNullException(nameof(syllabusRepository));
        }

        public async Task<ErrorOr<ListAllSyllabusesResponseApiDTO>> Handle(ListAllSyllabusesQuery request, CancellationToken cancellationToken)
        {
            var syllabuses = await _syllabusRepository.GetAllAsync();

            if (syllabuses == null || syllabuses.Count == 0)
            {
                return Error.NotFound(description: "No syllabuses found.");
            }

            var dto = new ListAllSyllabusesResponseApiDTO
            {
                Syllabuses = syllabuses.Select(s => new SyllabusResponseApiDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Courses = s.Courses.Select(c => new CourseResponseApiDTO
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Code = c.Code,
                        Semester = c.Semester,
                        Credits = c.Credits
                    }).ToList()
                }).ToList()
            };

            return dto;
        }
    }
}
