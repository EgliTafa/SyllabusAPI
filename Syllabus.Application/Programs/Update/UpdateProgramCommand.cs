using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Programs;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Programs.Update;

public record UpdateProgramCommand(UpdateProgramRequestApiDTO Request)
    : IRequest<ErrorOr<ProgramResponseApiDTO>>;

public class UpdateProgramCommandHandler
    : IRequestHandler<UpdateProgramCommand, ErrorOr<ProgramResponseApiDTO>>
{
    private readonly IProgramRepository _programRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateProgramCommandHandler(
        IProgramRepository programRepository,
        IDepartmentRepository departmentRepository)
    {
        _programRepository = programRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<ProgramResponseApiDTO>> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
    {
        var program = await _programRepository.GetByIdAsync(request.Request.Id);
        if (program is null)
        {
            return ProgramErrors.ProgramNotFound;
        }

        // Check if department exists
        var department = await _departmentRepository.GetByIdAsync(request.Request.DepartmentId);
        if (department is null)
        {
            return ProgramErrors.DepartmentNotFound;
        }

        // Check if another program with the same name already exists
        var existingProgram = await _programRepository.GetByNameAsync(request.Request.Name);
        if (existingProgram != null && existingProgram.Id != request.Request.Id)
        {
            return ProgramErrors.ProgramNameAlreadyExists;
        }

        program.Name = request.Request.Name;
        program.Description = request.Request.Description;
        program.DepartmentId = request.Request.DepartmentId;
        program.UpdatedAt = DateTime.UtcNow;

        await _programRepository.UpdateAsync(program);
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