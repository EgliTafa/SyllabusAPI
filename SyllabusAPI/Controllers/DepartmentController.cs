using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Syllabus.ApiContracts.Departments;
using Syllabus.Application.Departments.Create;
using Syllabus.Application.Departments.List;
using Syllabus.Application.Departments.GetById;
using Syllabus.Application.Departments.Update;
using Syllabus.Application.Departments.Delete;
using SyllabusAPI.Helpers;

namespace SyllabusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentRequestApiDTO request)
        {
            var command = new CreateDepartmentCommand(request);
            var result = await _mediator.Send(command);
            return result.ToCreatedAtActionResult(this, nameof(GetById), new { id = result.Value.Id });
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentRequestApiDTO request)
        {
            request.Id = id;
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