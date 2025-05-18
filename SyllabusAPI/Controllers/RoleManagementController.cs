using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syllabus.Application.Services;
using Syllabus.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Syllabus.Infrastructure.Data;
using Syllabus.Domain.Authentication;

namespace SyllabusAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class RoleManagementController : ControllerBase
{
    private readonly IRoleManagementService _roleManagementService;
    private readonly UserManager<UserEntity> _userManager;

    public RoleManagementController(
        IRoleManagementService roleManagementService,
        UserManager<UserEntity> userManager)
    {
        _roleManagementService = roleManagementService;
        _userManager = userManager;
    }

    [HttpGet("user-by-email")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound($"User with email {email} not found");
        }

        return Ok(new { userId = user.Id, email = user.Email });
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole(string userId, UserRole role)
    {
        var result = await _roleManagementService.AssignRoleToUserAsync(userId, role);
        if (!result)
        {
            return BadRequest("Failed to assign role to user");
        }
        return Ok();
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveRole(string userId, UserRole role)
    {
        var result = await _roleManagementService.RemoveRoleFromUserAsync(userId, role);
        if (!result)
        {
            return BadRequest("Failed to remove role from user");
        }
        return Ok();
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        var roles = await _roleManagementService.GetUserRolesAsync(userId);
        return Ok(roles);
    }

    [HttpGet("check")]
    public async Task<IActionResult> CheckUserRole(string userId, UserRole role)
    {
        var isInRole = await _roleManagementService.IsUserInRoleAsync(userId, role);
        return Ok(isInRole);
    }
} 