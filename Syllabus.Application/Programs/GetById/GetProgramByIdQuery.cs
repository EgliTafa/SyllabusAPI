using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Programs;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Programs.GetById;

public record GetProgramByIdQuery(int Id) : IRequest<ErrorOr<ProgramResponseApiDTO>>;

public class GetProgramByIdQueryHandler
    : IRequestHandler<GetProgramByIdQuery, ErrorOr<ProgramResponseApiDTO>>
{
    private readonly IProgramRepository _programRepository;

    public GetProgramByIdQueryHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<ErrorOr<ProgramResponseApiDTO>> Handle(GetProgramByIdQuery request, CancellationToken cancellationToken)
    {
        var program = await _programRepository.GetByIdAsync(request.Id);
        if (program is null)
        {
            return ProgramErrors.ProgramNotFound;
        }

        return new ProgramResponseApiDTO
        {
            Id = program.Id,
            Name = program.Name,
            Description = program.Description,
            DepartmentId = program.DepartmentId,
            DepartmentName = program.Department.Name,
            CreatedAt = program.CreatedAt,
            UpdatedAt = program.UpdatedAt,
            AcademicYears = program.AcademicYears.Select(ay => new ProgramAcademicYearDTO
            {
                Id = ay.Id,
                AcademicYear = ay.AcademicYear
            }).ToList()
        };
    }
} 