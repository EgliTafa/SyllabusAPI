using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace SyllabusAPI.Helpers
{
    public static class ResultHelper
    {
        public static IActionResult ToActionResult<T>(this ErrorOr<T> result, ControllerBase controller)
        {
            return result.Match(
                success => controller.Ok(success),
                error => controller.Problem(
                    detail: error.FirstOrDefault().Description,
                    statusCode: error.FirstOrDefault().Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status400BadRequest
                    }
                )
            );
        }

        public static IActionResult ToNoContentResult(this ErrorOr<Deleted> result, ControllerBase controller)
        {
            return result.Match<IActionResult>(
                _ => controller.NoContent(),
                error => controller.Problem(
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
