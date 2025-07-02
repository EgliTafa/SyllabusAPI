using ErrorOr;
using MediatR;
using Syllabus.ApiContracts.Departments;
using Syllabus.Domain.Sylabusses;

namespace Syllabus.Application.Departments.List
{
    public record ListDepartmentsQuery : IRequest<ErrorOr<List<DepartmentResponseApiDTO>>>;

    public class ListDepartmentsQueryHandler : IRequestHandler<ListDepartmentsQuery, ErrorOr<List<DepartmentResponseApiDTO>>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public ListDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ErrorOr<List<DepartmentResponseApiDTO>>> Handle(ListDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetAllAsync();

            var response = departments.Select(d => new DepartmentResponseApiDTO
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            }).ToList();

            return response;
        }
    }
} 