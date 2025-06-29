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

        public static IActionResult ToCreatedAtActionResult<T>(this ErrorOr<T> result, ControllerBase controller, string actionName, object routeValues)
        {
            return result.Match(
                success => controller.CreatedAtAction(actionName, routeValues, success),
                error => controller.Problem(
                    detail: error.FirstOrDefault().Description,
                    statusCode: error.FirstOrDefault().Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        ErrorType.Conflict => StatusCodes.Status409Conflict,
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

        public static IActionResult ToNoContentResult(this ErrorOr<bool> result, ControllerBase controller)
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

        public static IActionResult ToNoContentResult(this ErrorOr<Success> result, ControllerBase controller)
        {
            return result.Match<IActionResult>(
                _ => controller.NoContent(),
                error => controller.Problem(
                    detail: error.FirstOrDefault().Description,
                    statusCode: error.FirstOrDefault().Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        ErrorType.Conflict => StatusCodes.Status409Conflict,
                        _ => StatusCodes.Status400BadRequest
                    }
                )
            );
        }

        public static IActionResult ToProblemResult<T>(this ErrorOr<T> result, ControllerBase controller)
        {
            if (result.IsError)
            {
                var error = result.Errors.FirstOrDefault();
                return controller.Problem(
                    detail: error.Description,
                    statusCode: error.Type switch
                    {
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status400BadRequest
                    }
                );
            }

            return controller.BadRequest("ToProblemResult was called on a successful result.");
        }
    }
}
