using Microsoft.AspNetCore.Identity;
using Syllabus.Domain.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Services;

public class RoleManagementService : IRoleManagementService
{
    private readonly UserManager<UserEntity> _userManager;

    public RoleManagementService(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> AssignRoleToUserAsync(string userId, UserRole role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var result = await _userManager.AddToRoleAsync(user, role.ToString());
        return result.Succeeded;
    }

    public async Task<bool> RemoveRoleFromUserAsync(string userId, UserRole role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var result = await _userManager.RemoveFromRoleAsync(user, role.ToString());
        return result.Succeeded;
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Enumerable.Empty<string>();

        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> IsUserInRoleAsync(string userId, UserRole role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        return await _userManager.IsInRoleAsync(user, role.ToString());
    }
} 