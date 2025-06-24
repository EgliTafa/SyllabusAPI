using ErrorOr;
using MediatR;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Departments.Delete;

public record DeleteDepartmentCommand(int Id) : IRequest<ErrorOr<Deleted>>;

public class DeleteDepartmentCommandHandler
    : IRequestHandler<DeleteDepartmentCommand, ErrorOr<Deleted>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(request.Id);
        if (department is null)
        {
            return DepartmentErrors.DepartmentNotFound;
        }

        // Check if department has programs
        if (department.Programs.Any())
        {
            return DepartmentErrors.DepartmentHasPrograms;
        }

        await _departmentRepository.DeleteAsync(department);
        await _departmentRepository.SaveChangesAsync();

        return Result.Deleted;
    }
} 