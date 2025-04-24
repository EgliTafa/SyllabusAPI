using MediatR;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Application.Syllabus.Create;
using Syllabus.Application.Syllabus.Delete;
using Syllabus.Application.Syllabus.GetById;
using Syllabus.Application.Syllabus.List;
using SyllabusAPI.Helpers;
using SyllabusApplication.Syllabuses.Commands;

namespace SyllabusAPI.Controllers
{
    public class SyllabusController : Controller
    {
        private readonly ISender _mediator;

        public SyllabusController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("syllabuses")]
        public async Task<IActionResult> ListAllSyllabuses([FromQuery] ListAllSyllabusesRequestApiDTO request)
        {
            var result = await _mediator.Send(new ListAllSyllabusesQuery(request));
            return result.ToActionResult(this);
        }

        [HttpGet("syllabuses/{syllabusId:int}")]
        public async Task<IActionResult> GetSyllabusById([FromRoute] int syllabusId)
        {
            var request = new GetSyllabusByIdRequestApiDTO { SyllabusId = syllabusId };
            var result = await _mediator.Send(new GetSyllabusByIdQuery(request));
            return result.ToActionResult(this);
        }

        [HttpPost("syllabuses")]
        public async Task<IActionResult> CreateSyllabus([FromBody] CreateSyllabusRequestApiDTO request)
        {
            var result = await _mediator.Send(new CreateSyllabusCommand(request));
            return result.ToActionResult(this);
        }

        [HttpDelete("{syllabusId:int}")]
        public async Task<IActionResult> DeleteSyllabus([FromRoute] int syllabusId)
        {
            var request = new DeleteSyllabusRequestApiDTO { SyllabusId = syllabusId };
            var result = await _mediator.Send(new DeleteSyllabusCommand(request));
            return result.ToNoContentResult(this);
        }

        [HttpPut("syllabuses/{syllabusId:int}")]
        public async Task<IActionResult> RenameSyllabus([FromRoute] int syllabusId, [FromBody] UpdateSyllabusRequestApiDTO request)
        {
            if (request.SyllabusId != syllabusId)
            {
                return BadRequest("Syllabus ID in the route and payload must match.");
            }

            var result = await _mediator.Send(new UpdateSyllabusCommand(request));
            return result.ToActionResult(this);
        }

        [HttpPut("syllabuses/{syllabusId:int}/courses")]
        public async Task<IActionResult> AddOrRemoveCoursesFromSyllabus([FromRoute] int syllabusId, [FromBody] AddOrRemoveCoursesFromSyllabusRequestApiDTO request)
        {
            if (request.SyllabusId != syllabusId)
            {
                return BadRequest("Syllabus ID mismatch.");
            }

            var result = await _mediator.Send(new AddOrRemoveCoursesFromSyllabusCommand(request));
            return result.ToActionResult(this);
        }

    }
}
