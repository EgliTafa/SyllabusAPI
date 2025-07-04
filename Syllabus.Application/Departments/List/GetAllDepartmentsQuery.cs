using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Departments;
using Syllabus.ApiContracts.Programs;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Departments.List;

public record GetAllDepartmentsQuery : IRequest<ErrorOr<List<DepartmentResponseApiDTO>>>;

public class GetAllDepartmentsQueryHandler
    : IRequestHandler<GetAllDepartmentsQuery, ErrorOr<List<DepartmentResponseApiDTO>>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<List<DepartmentResponseApiDTO>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAllAsync();

        var response = departments.Select(d => new DepartmentResponseApiDTO
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            CreatedAt = d.CreatedAt,
            UpdatedAt = d.UpdatedAt,
            Programs = d.Programs.Select(p => new ProgramResponseApiDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DepartmentId = p.DepartmentId,
                DepartmentName = d.Name,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                AcademicYears = p.AcademicYears.Select(ay => new ProgramAcademicYearDTO
                {
                    Id = ay.Id,
                    AcademicYear = ay.AcademicYear
                }).ToList()
            }).ToList()
        }).ToList();

        return response;
    }
} 