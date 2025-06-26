using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Programs;
using Syllabus.Application.Programs.Create;
using Syllabus.Application.Programs.Delete;
using Syllabus.Application.Programs.GetById;
using Syllabus.Application.Programs.List;
using Syllabus.Application.Programs.Update;
using SyllabusAPI.Helpers;

namespace SyllabusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class ProgramController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProgramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProgramRequestApiDTO request)
        {
            var command = new CreateProgramCommand(request);
            var result = await _mediator.Send(command);
            return result.ToCreatedAtActionResult(this, nameof(GetById), new { id = result.Value.Id });
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var query = new GetAllProgramsQuery();
            var result = await _mediator.Send(query);
            return result.ToActionResult(this);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProgramByIdQuery(id);
            var result = await _mediator.Send(query);
            return result.ToActionResult(this);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProgramRequestApiDTO request)
        {
            request.Id = id;
            var command = new UpdateProgramCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult(this);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProgramCommand(id);
            var result = await _mediator.Send(command);
            return result.ToNoContentResult(this);
        }
    }
} 