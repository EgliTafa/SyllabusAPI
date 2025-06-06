using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.Application.Syllabus.Create;
using Syllabus.Application.Syllabus.Delete;
using Syllabus.Application.Syllabus.Export;
using Syllabus.Application.Syllabus.GetById;
using Syllabus.Application.Syllabus.List;
using Syllabus.Domain.Users;
using SyllabusAPI.Helpers;
using SyllabusApplication.Syllabuses.Commands;

namespace SyllabusAPI.Controllers
{
    /// <summary>
    /// Handles syllabus-related operations like create, list, delete, and update.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SyllabusController : ControllerBase
    {
        private readonly ISender _mediator;

        /// <summary>
        /// Constructor for SyllabusController.
        /// </summary>
        /// <param name="mediator">MediatR sender for dispatching commands and queries.</param>
        public SyllabusController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Lists all syllabuses.
        /// </summary>
        /// <param name="request">Filter and pagination parameters.</param>
        /// <returns>List of syllabuses.</returns>
        [HttpGet()]
        [Authorize(Roles = nameof(UserRole.Student) + "," + nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> ListAllSyllabuses([FromQuery] ListAllSyllabusesRequestApiDTO request)
        {
            var result = await _mediator.Send(new ListAllSyllabusesQuery(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Retrieves a syllabus by its ID.
        /// </summary>
        /// <param name="syllabusId">The ID of the syllabus to retrieve.</param>
        /// <returns>The requested syllabus if found.</returns>
        [HttpGet("{syllabusId:int}")]
        [Authorize(Roles = nameof(UserRole.Student) + "," + nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> GetSyllabusById([FromRoute] int syllabusId)
        {
            var request = new GetSyllabusByIdRequestApiDTO { SyllabusId = syllabusId };
            var result = await _mediator.Send(new GetSyllabusByIdQuery(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Creates a new syllabus.
        /// </summary>
        /// <param name="request">The syllabus creation request payload.</param>
        /// <returns>Result of the creation operation.</returns>
        [HttpPost()]
        [Authorize(Roles = nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> CreateSyllabus([FromBody] CreateSyllabusRequestApiDTO request)
        {
            var result = await _mediator.Send(new CreateSyllabusCommand(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Deletes a syllabus by its ID.
        /// </summary>
        /// <param name="syllabusId">The ID of the syllabus to delete.</param>
        /// <returns>No content if deletion was successful.</returns>
        [HttpDelete("{syllabusId:int}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> DeleteSyllabus([FromRoute] int syllabusId)
        {
            var request = new DeleteSyllabusRequestApiDTO { SyllabusId = syllabusId };
            var result = await _mediator.Send(new DeleteSyllabusCommand(request));
            return result.ToNoContentResult(this);
        }

        /// <summary>
        /// Updates the name of an existing syllabus.
        /// </summary>
        /// <param name="syllabusId">The ID of the syllabus to update.</param>
        /// <param name="request">The update payload with the new name.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut("{syllabusId:int}")]
        [Authorize(Roles = nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> RenameSyllabus([FromRoute] int syllabusId, [FromBody] UpdateSyllabusRequestApiDTO request)
        {
            if (request.SyllabusId != syllabusId)
            {
                return BadRequest("Syllabus ID in the route and payload must match.");
            }

            var result = await _mediator.Send(new UpdateSyllabusCommand(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Adds or removes courses from a syllabus.
        /// </summary>
        /// <param name="syllabusId">The ID of the syllabus.</param>
        /// <param name="request">List of course IDs to add or remove.</param>
        /// <returns>Result of the operation.</returns>
        [HttpPut("{syllabusId:int}/courses")]
        [Authorize(Roles = nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> AddOrRemoveCoursesFromSyllabus([FromRoute] int syllabusId, [FromBody] AddOrRemoveCoursesFromSyllabusRequestApiDTO request)
        {
            if (request.SyllabusId != syllabusId)
            {
                return BadRequest("Syllabus ID mismatch.");
            }

            var result = await _mediator.Send(new AddOrRemoveCoursesFromSyllabusCommand(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Exports the syllabus as a PDF document.
        /// </summary>
        /// <param name="syllabusId">The ID of the syllabus to export.</param>
        /// <returns>PDF file of the syllabus.</returns>
        [HttpGet("{syllabusId:int}/export-pdf")]
        [Authorize(Roles = nameof(UserRole.Student) + "," + nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> ExportSyllabusPdf([FromRoute] int syllabusId)
        {
            var request = new ExportSyllabusPdfRequestApiDTO { SyllabusId = syllabusId };
            var result = await _mediator.Send(new ExportSyllabusPdfCommand(request));

            if (result.IsError)
                return result.ToActionResult(this);

            return File(result.Value.FileBytes, result.Value.ContentType, result.Value.FileName);
        }
    }
}
