using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Departments;
using Syllabus.ApiContracts.Programs;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Departments.GetById;

public record GetDepartmentByIdQuery(int Id) : IRequest<ErrorOr<DepartmentResponseApiDTO>>;

public class GetDepartmentByIdQueryHandler
    : IRequestHandler<GetDepartmentByIdQuery, ErrorOr<DepartmentResponseApiDTO>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<DepartmentResponseApiDTO>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(request.Id);
        if (department is null)
        {
            return DepartmentErrors.DepartmentNotFound;
        }

        return new DepartmentResponseApiDTO
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            CreatedAt = department.CreatedAt,
            UpdatedAt = department.UpdatedAt,
            Programs = department.Programs.Select(p => new ProgramResponseApiDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DepartmentId = p.DepartmentId,
                DepartmentName = department.Name,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList()
        };
    }
} 