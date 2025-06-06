using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Courses;
using Syllabus.Application.Courses.AddDetail;
using Syllabus.Application.Courses.Create;
using Syllabus.Application.Courses.Delete;
using Syllabus.Application.Courses.GetById;
using Syllabus.Application.Courses.Update;
using Syllabus.Application.Courses.UpdateDetail;
using Syllabus.Domain.Users;
using SyllabusAPI.Helpers;
using SyllabusApplication.Courses.Queries;

namespace SyllabusAPI.Controllers
{
    /// <summary>
    /// Manages course-related operations including creation, retrieval, update, and deletion of courses.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ISender _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoursesController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR sender for dispatching commands and queries.</param>
        public CoursesController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieves the list of all courses.
        /// </summary>
        /// <param name="request">Optional query parameters for filtering or pagination.</param>
        /// <returns>A list of all available courses.</returns>
        [HttpGet()]
        [Authorize(Roles = nameof(UserRole.Student) + "," + nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> ListAllCourses([FromQuery] ListAllCoursesRequestApiDTO request)
        {
            var result = await _mediator.Send(new ListAllCoursesQuery(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Retrieves details of a specific course by its ID.
        /// </summary>
        /// <param name="courseId">The ID of the course to retrieve.</param>
        /// <returns>The course details if found.</returns>
        [HttpGet("{courseId:int}")]
        [Authorize(Roles = nameof(UserRole.Student) + "," + nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> GetCourseById([FromRoute] int courseId)
        {
            var request = new GetCourseByIdRequest { CourseId = courseId };
            var result = await _mediator.Send(new GetCourseByIdQuery(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        /// <param name="request">The course creation request payload.</param>
        /// <returns>The result of the creation operation.</returns>
        [HttpPost()]
        [Authorize(Roles = nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequestApiDTO request)
        {
            var result = await _mediator.Send(new CreateCourseCommand(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Deletes a course by its ID.
        /// </summary>
        /// <returns>No content if the deletion was successful.</returns>
        [HttpDelete("{courseId:int}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> DeleteCourse([FromBody] DeleteCourseRequestApiDTO request)
        {
            if (request.CourseId != request.CourseId)
            {
                return BadRequest("Course ID in URL and payload do not match.");
            }

            var result = await _mediator.Send(new DeleteCourseCommand(request));
            return result.ToNoContentResult(this);
        }

        /// <summary>
        /// Updates an existing course by its ID.
        /// </summary>
        /// <param name="courseId">The ID of the course to update (from URL).</param>
        /// <param name="request">The update request payload containing the new values.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPut("{courseId:int}")]
        [Authorize(Roles = nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromBody] UpdateCourseRequestApiDTO request)
        {
            if (request.CourseId != courseId)
            {
                return BadRequest("Course ID in URL and payload do not match.");
            }

            var result = await _mediator.Send(new UpdateCourseCommand(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Adds details to an existing course.
        /// </summary>
        /// <param name="courseId">The ID of the course to add details to.</param>
        /// <param name="request">The course details to add.</param>
        /// <returns>The updated course with its new details.</returns>
        [HttpPost("{courseId:int}/details")]
        [Authorize(Roles = nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> AddCourseDetails(int courseId, [FromBody] CourseDetailRequestApiDTO request)
        {
            var result = await _mediator.Send(new AddCourseDetailCommand(courseId, request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Updates the details of an existing course.
        /// </summary>
        /// <param name="courseId">The ID of the course whose details to update.</param>
        /// <param name="request">The updated course details.</param>
        /// <returns>The updated course with its modified details.</returns>
        [HttpPut("{courseId:int}/details")]
        [Authorize(Roles = nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> UpdateCourseDetails(int courseId, [FromBody] CourseDetailRequestApiDTO request)
        {
            var result = await _mediator.Send(new UpdateCourseDetailCommand(courseId, request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Exports a single course as a PDF document.
        /// </summary>
        /// <param name="courseId">The ID of the course to export.</param>
        /// <returns>PDF file of the course.</returns>
        [HttpGet("{courseId:int}/export-pdf")]
        [Authorize(Roles = nameof(UserRole.Student) + "," + nameof(UserRole.Professor) + "," + nameof(UserRole.Administrator))]
        public async Task<IActionResult> ExportCoursePdf([FromRoute] int courseId)
        {
            var result = await _mediator.Send(new Syllabus.Application.Courses.Export.ExportCoursePdfCommand(courseId));
            if (result.IsError)
                return result.ToActionResult(this);
            return File(result.Value.FileBytes, result.Value.ContentType, result.Value.FileName);
        }
    }
}
