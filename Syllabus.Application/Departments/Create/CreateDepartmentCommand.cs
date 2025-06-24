using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Departments;
using Syllabus.ApiContracts.Programs;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Departments.Create;

public record CreateDepartmentCommand(CreateDepartmentRequestApiDTO Request)
    : IRequest<ErrorOr<DepartmentResponseApiDTO>>;

public class CreateDepartmentCommandHandler
    : IRequestHandler<CreateDepartmentCommand, ErrorOr<DepartmentResponseApiDTO>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<DepartmentResponseApiDTO>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        // Check if department with same name already exists
        var existingDepartment = await _departmentRepository.GetByNameAsync(request.Request.Name);
        if (existingDepartment != null)
        {
            return DepartmentErrors.DepartmentNameAlreadyExists;
        }

        var department = new Department
        {
            Name = request.Request.Name,
            Description = request.Request.Description
        };

        await _departmentRepository.AddAsync(department);
        await _departmentRepository.SaveChangesAsync();

        return new DepartmentResponseApiDTO
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            CreatedAt = department.CreatedAt,
            UpdatedAt = department.UpdatedAt,
            Programs = new List<ProgramResponseApiDTO>()
        };
    }
} 