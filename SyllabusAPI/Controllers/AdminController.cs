using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Application.Authentication.CreateUserByAdmin;
using Syllabus.Application.Authentication.GetAllUsers;
using Syllabus.Application.Authentication.RevokeUserAccess;
using Syllabus.Application.Authentication.RestoreUserAccess;
using Syllabus.Application.Authentication.DeleteUser;
using Syllabus.Application.Authentication.SearchUsers;
using Syllabus.Application.Authentication.UpdateUserByAdmin;
using SyllabusAPI.Helpers;

namespace SyllabusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all users in the system
    /// </summary>
    [HttpGet("users")]
    [ProducesResponseType(typeof(List<GetAllUsersResponseApiDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllUsers()
    {
        var command = new GetAllUsersCommand();
        var result = await _mediator.Send(command);

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Search users by email or name
    /// </summary>
    [HttpGet("users/search")]
    [ProducesResponseType(typeof(List<SearchUsersResponseApiDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> SearchUsers([FromQuery] string query)
    {
        var command = new SearchUsersCommand(query);
        var result = await _mediator.Send(command);

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Create a new user by administrator
    /// </summary>
    [HttpPost("users")]
    [ProducesResponseType(typeof(CreateUserByAdminResponseApiDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserByAdminRequestApiDTO request)
    {
        var command = new CreateUserByAdminCommand(request);
        var result = await _mediator.Send(command);

        return result.ToCreatedAtActionResult(this, nameof(GetAllUsers), new { });
    }

    /// <summary>
    /// Update a user by administrator
    /// </summary>
    [HttpPut("users/{userId}")]
    [ProducesResponseType(typeof(UpdateUserByAdminResponseApiDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserByAdminRequestApiDTO request)
    {
        var command = new UpdateUserByAdminCommand(userId, request);
        var result = await _mediator.Send(command);

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Revoke user access (lockout user)
    /// </summary>
    [HttpPost("users/{userId}/revoke")]
    [ProducesResponseType(typeof(RevokeUserAccessResponseApiDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RevokeUserAccess(string userId, [FromBody] RevokeUserAccessRequestApiDTO request)
    {
        var command = new RevokeUserAccessCommand(userId, request);
        var result = await _mediator.Send(command);

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Restore user access (remove lockout)
    /// </summary>
    [HttpPost("users/{userId}/restore")]
    [ProducesResponseType(typeof(RestoreUserAccessResponseApiDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RestoreUserAccess(string userId)
    {
        var command = new RestoreUserAccessCommand(userId);
        var result = await _mediator.Send(command);

        return result.ToActionResult(this);
    }

    /// <summary>
    /// Delete a user permanently
    /// </summary>
    [HttpDelete("users/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var command = new DeleteUserCommand(userId);
        var result = await _mediator.Send(command);

        return result.ToNoContentResult(this);
    }
} 