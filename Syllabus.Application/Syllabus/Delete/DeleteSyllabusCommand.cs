using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Syllabus.Delete
{
    public record DeleteSyllabusCommand(DeleteSyllabusRequestApiDTO Request) : IRequest<ErrorOr<Deleted>>;

    public class DeleteSyllabusCommandHandler : IRequestHandler<DeleteSyllabusCommand, ErrorOr<Deleted>>
    {
        private readonly ISyllabusRepository _syllabusRepository;
        public DeleteSyllabusCommandHandler(ISyllabusRepository syllabusRepository)
        {
            _syllabusRepository = syllabusRepository ?? throw new ArgumentNullException(nameof(syllabusRepository));
        }
        public async Task<ErrorOr<Deleted>> Handle(DeleteSyllabusCommand request, CancellationToken cancellationToken)
        {
            var syllabus = await _syllabusRepository.GetByIdAsync(request.Request.SyllabusId);
            if (syllabus is null)
            {
                return Error.NotFound(description: $"Syllabus with ID {request.Request.SyllabusId} not found.");
            }
            _syllabusRepository.Remove(syllabus);
            await _syllabusRepository.SaveChangesAsync();
            return Result.Deleted;
        }
    }
}
