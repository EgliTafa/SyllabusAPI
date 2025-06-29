using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Programs;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Programs;

public record GetProgramAcademicYearsQuery(int? DepartmentId = null) : IRequest<ErrorOr<List<ProgramAcademicYearResponseApiDTO>>>;

public class GetProgramAcademicYearsQueryHandler
    : IRequestHandler<GetProgramAcademicYearsQuery, ErrorOr<List<ProgramAcademicYearResponseApiDTO>>>
{
    private readonly IProgramRepository _programRepository;

    public GetProgramAcademicYearsQueryHandler(IProgramRepository programRepository)
    {
        _programRepository = programRepository;
    }

    public async Task<ErrorOr<List<ProgramAcademicYearResponseApiDTO>>> Handle(GetProgramAcademicYearsQuery request, CancellationToken cancellationToken)
    {
        List<Program> programs;
        
        if (request.DepartmentId.HasValue)
        {
            programs = await _programRepository.GetByDepartmentIdAsync(request.DepartmentId.Value);
        }
        else
        {
            programs = await _programRepository.GetAllAsync();
        }

        var response = programs
            .SelectMany(p => p.AcademicYears.Select(ay => new ProgramAcademicYearResponseApiDTO
            {
                Id = ay.Id,
                AcademicYear = ay.AcademicYear,
                Program = new ProgramResponseApiDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DepartmentId = p.DepartmentId,
                    DepartmentName = p.Department.Name,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    AcademicYears = p.AcademicYears.Select(ay => new ProgramAcademicYearDTO
                    {
                        Id = ay.Id,
                        AcademicYear = ay.AcademicYear
                    }).ToList()
                }
            }))
            .ToList();

        return response;
    }
} 