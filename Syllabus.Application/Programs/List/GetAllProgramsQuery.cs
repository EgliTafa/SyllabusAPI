using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Programs;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Programs.List;

public record GetAllProgramsQuery : IRequest<ErrorOr<List<ProgramResponseApiDTO>>>;

public class GetAllProgramsQueryHandler
    : IRequestHandler<GetAllProgramsQuery, ErrorOr<List<ProgramResponseApiDTO>>>
{
    private readonly IProgramRepository _programRepository;

    public GetAllProgramsQueryHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<ErrorOr<List<ProgramResponseApiDTO>>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
    {
        var programs = await _programRepository.GetAllAsync();

        var response = programs.Select(p => new ProgramResponseApiDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            DepartmentId = p.DepartmentId,
            DepartmentName = p.Department.Name,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        }).ToList();

        return response;
    }
} 