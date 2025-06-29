using ErrorOr;
using MediatR;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Programs.Delete;

public record DeleteProgramCommand(int Id) : IRequest<ErrorOr<Deleted>>;

public class DeleteProgramCommandHandler
    : IRequestHandler<DeleteProgramCommand, ErrorOr<Deleted>>
{
    private readonly IProgramRepository _programRepository;

    public DeleteProgramCommandHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteProgramCommand request, CancellationToken cancellationToken)
    {
        var program = await _programRepository.GetByIdAsync(request.Id);
        if (program is null)
        {
            return ProgramErrors.ProgramNotFound;
        }

        // Check if program has syllabuses
        if (program.AcademicYears.Any(ay => ay.Syllabuses.Any()))
        {
            return ProgramErrors.ProgramHasSyllabuses;
        }

        await _programRepository.DeleteAsync(program);
        await _programRepository.SaveChangesAsync();

        return Result.Deleted;
    }
} 