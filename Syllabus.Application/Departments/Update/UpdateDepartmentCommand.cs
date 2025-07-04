using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Departments;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Departments.Update;

public record UpdateDepartmentCommand(UpdateDepartmentRequestApiDTO Request)
    : IRequest<ErrorOr<DepartmentResponseApiDTO>>;

public class UpdateDepartmentCommandHandler
    : IRequestHandler<UpdateDepartmentCommand, ErrorOr<DepartmentResponseApiDTO>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<DepartmentResponseApiDTO>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(request.Request.Id);
        if (department is null)
        {
            return DepartmentErrors.DepartmentNotFound;
        }

        // Check if another department with the same name already exists
        var existingDepartment = await _departmentRepository.GetByNameAsync(request.Request.Name);
        if (existingDepartment != null && existingDepartment.Id != request.Request.Id)
        {
            return DepartmentErrors.DepartmentNameAlreadyExists;
        }

        department.Name = request.Request.Name;
        department.Description = request.Request.Description;
        department.UpdatedAt = DateTime.UtcNow;

        await _departmentRepository.UpdateAsync(department);
        await _departmentRepository.SaveChangesAsync();

        return new DepartmentResponseApiDTO
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            CreatedAt = department.CreatedAt,
            UpdatedAt = department.UpdatedAt
        };
    }
} 