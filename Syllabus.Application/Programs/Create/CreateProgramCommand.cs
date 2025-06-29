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

        // Validate academic year format (YYYY-YYYY)
        if (!IsValidAcademicYearFormat(request.Request.AcademicYear))
        {
            return ProgramErrors.InvalidAcademicYearFormat;
        }

        // Check if program with same name and academic year already exists
        var existingProgram = await _programRepository.GetByNameAndAcademicYearAsync(request.Request.Name, request.Request.AcademicYear);
        if (existingProgram != null)
        {
            return ProgramErrors.ProgramNameAndYearAlreadyExists;
        }

        var program = new Program
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            DepartmentId = request.Request.DepartmentId
        };

        await _programRepository.AddAsync(program);
        await _programRepository.SaveChangesAsync();

        // Create ProgramAcademicYear
        var academicYear = new ProgramAcademicYear
        {
            ProgramId = program.Id,
            AcademicYear = request.Request.AcademicYear
        };
        program.AcademicYears.Add(academicYear);
        await _programRepository.SaveChangesAsync();

        return new ProgramResponseApiDTO
        {
            Id = program.Id,
            Name = program.Name,
            Description = program.Description,
            DepartmentId = program.DepartmentId,
            DepartmentName = department.Name,
            CreatedAt = program.CreatedAt,
            UpdatedAt = program.UpdatedAt,
            AcademicYears = program.AcademicYears.Select(ay => new ProgramAcademicYearDTO
            {
                Id = ay.Id,
                AcademicYear = ay.AcademicYear
            }).ToList()
        };
    }

    private bool IsValidAcademicYearFormat(string academicYear)
    {
        if (string.IsNullOrWhiteSpace(academicYear))
            return false;

        var parts = academicYear.Split('-');
        if (parts.Length != 2)
            return false;

        if (!int.TryParse(parts[0], out int startYear) || !int.TryParse(parts[1], out int endYear))
            return false;

        // Validate that end year is start year + 3 (3-year program)
        return endYear == startYear + 3;
    }
} 