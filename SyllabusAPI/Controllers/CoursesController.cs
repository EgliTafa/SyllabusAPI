using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Courses;
using Syllabus.Application.Courses.Create;
using Syllabus.Application.Courses.Delete;
using Syllabus.Application.Courses.GetById;
using Syllabus.Application.Courses.Update;
using SyllabusApplication.Courses.Queries;

namespace SyllabusAPI.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ISender _mediator;

        public CoursesController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("courses")]
        public async Task<IActionResult> ListAllCourses(ListAllCoursesRequestApiDTO request)
        {
            var result = await _mediator.Send(new ListAllCoursesQuery(request));

            return result.Match(
                success => Ok(success),
                error => Problem(
                    detail: error.FirstOrDefault().Description,
                    statusCode: error.FirstOrDefault().Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status400BadRequest
                    }
                )
            );
        }

        [HttpGet("courses/{courseId:int}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var request = new GetCourseByIdRequest { CourseId = courseId };
            var result = await _mediator.Send(new GetCourseByIdQuery(request));

            return result.Match(
                success => Ok(success),
                error => Problem(
                    detail: error.FirstOrDefault().Description,
                    statusCode: error.FirstOrDefault().Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status400BadRequest
                    }
                )
            );
        }

        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourse(CreateCourseRequestApiDTO request)
        {
            var result = await _mediator.Send(new CreateCourseCommand(request));
            return result.Match(
                success => CreatedAtAction(nameof(GetCourseById), new { courseId = success.Id }, success),
                error => Problem(
                    detail: error.FirstOrDefault().Description,
                    statusCode: error.FirstOrDefault().Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status400BadRequest
                    }
                )
            );
        }

        [HttpDelete("courses/{courseId:int}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var request = new DeleteCourseRequestApiDTO { CourseId = courseId };
            var result = await _mediator.Send(new DeleteCourseCommand(request));

            return result.Match<IActionResult>(
                _ => NoContent(),
                error => Problem(
                    detail: error.FirstOrDefault().Description,
                    statusCode: error.FirstOrDefault().Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status400BadRequest
                    }
                )
            );
        }

        [HttpPut("courses/{courseId:int}")]
        public async Task<IActionResult> UpdateCourse(int courseId, [FromBody] UpdateCourseRequestApiDTO request)
        {
            if (request.CourseId != courseId)
            {
                return BadRequest("Course ID in URL and payload do not match.");
            }

            var result = await _mediator.Send(new UpdateCourseCommand(request));

            return result.Match(
                success => Ok(success),
                error => Problem(
                    detail: error.FirstOrDefault().Description,
                    statusCode: error.FirstOrDefault().Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status400BadRequest
                    }
                )
            );
        }

    }
}
