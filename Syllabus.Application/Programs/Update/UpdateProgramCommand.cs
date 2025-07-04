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

        // Validate academic year format (YYYY-YYYY)
        if (!IsValidAcademicYearFormat(request.Request.AcademicYear))
        {
            return ProgramErrors.InvalidAcademicYearFormat;
        }

        // Check if another program with same name and academic year already exists
        var existingProgram = await _programRepository.GetByNameAndAcademicYearAsync(request.Request.Name, request.Request.AcademicYear);
        if (existingProgram != null && existingProgram.Id != request.Request.Id)
        {
            return ProgramErrors.ProgramNameAndYearAlreadyExists;
        }

        program.Name = request.Request.Name;
        program.Description = request.Request.Description;
        program.DepartmentId = request.Request.DepartmentId;
        program.UpdatedAt = DateTime.UtcNow;

        // Update or add ProgramAcademicYear
        var existingYear = program.AcademicYears.FirstOrDefault(ay => ay.AcademicYear == request.Request.AcademicYear);
        if (existingYear == null)
        {
            var newYear = new ProgramAcademicYear
            {
                ProgramId = program.Id,
                AcademicYear = request.Request.AcademicYear
            };
            program.AcademicYears.Add(newYear);
        }
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