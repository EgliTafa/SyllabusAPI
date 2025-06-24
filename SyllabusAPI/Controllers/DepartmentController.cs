using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Departments;
using Syllabus.Application.Departments.Create;
using Syllabus.Application.Departments.Delete;
using Syllabus.Application.Departments.GetById;
using Syllabus.Application.Departments.List;
using Syllabus.Application.Departments.Update;
using SyllabusAPI.Helpers;

namespace SyllabusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentRequestApiDTO request)
        {
            var command = new CreateDepartmentCommand(request);
            var result = await _mediator.Send(command);

            return result.ToCreatedAtActionResult(this, nameof(GetById), new { id = result.Value.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _mediator.Send(query);

            return result.ToActionResult(this);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetDepartmentByIdQuery(id);
            var result = await _mediator.Send(query);

            return result.ToActionResult(this);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDepartmentRequestApiDTO request)
        {
            var command = new UpdateDepartmentCommand(request);
            var result = await _mediator.Send(command);

            return result.ToActionResult(this);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDepartmentCommand(id);
            var result = await _mediator.Send(command);

            return result.ToNoContentResult(this);
        }
    }
} 