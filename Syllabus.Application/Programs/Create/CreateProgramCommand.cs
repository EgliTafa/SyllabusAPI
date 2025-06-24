using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Programs;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Programs.Create;

public record CreateProgramCommand(CreateProgramRequestApiDTO Request)
    : IRequest<ErrorOr<ProgramResponseApiDTO>>;

public class CreateProgramCommandHandler
    : IRequestHandler<CreateProgramCommand, ErrorOr<ProgramResponseApiDTO>>
{
    private readonly IProgramRepository _programRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public CreateProgramCommandHandler(
        IProgramRepository programRepository,
        IDepartmentRepository departmentRepository)
    {
        _programRepository = programRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<ProgramResponseApiDTO>> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
    {
        // Check if department exists
        var department = await _departmentRepository.GetByIdAsync(request.Request.DepartmentId);
        if (department is null)
        {
            return ProgramErrors.DepartmentNotFound;
        }

        // Check if program with same name already exists
        var existingProgram = await _programRepository.GetByNameAsync(request.Request.Name);
        if (existingProgram != null)
        {
            return ProgramErrors.ProgramNameAlreadyExists;
        }

        var program = new Program
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            DepartmentId = request.Request.DepartmentId
        };

        await _programRepository.AddAsync(program);
        await _programRepository.SaveChangesAsync();

        return new ProgramResponseApiDTO
        {
            Id = program.Id,
            Name = program.Name,
            Description = program.Description,
            DepartmentId = program.DepartmentId,
            DepartmentName = department.Name,
            CreatedAt = program.CreatedAt,
            UpdatedAt = program.UpdatedAt
        };
    }
} 