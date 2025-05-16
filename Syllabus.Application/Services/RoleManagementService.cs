using Microsoft.AspNetCore.Identity;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Services;

public interface IRoleManagementService
{
    Task<bool> AssignRoleToUserAsync(string userId, UserRole role);
    Task<bool> RemoveRoleFromUserAsync(string userId, UserRole role);
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task<bool> IsUserInRoleAsync(string userId, UserRole role);
}

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
        {
            return false;
        }

        var result = await _userManager.AddToRoleAsync(user, role.ToString());
        return result.Succeeded;
    }

    public async Task<bool> RemoveRoleFromUserAsync(string userId, UserRole role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        var result = await _userManager.RemoveFromRoleAsync(user, role.ToString());
        return result.Succeeded;
    }

    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new List<string>();
        }

        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> IsUserInRoleAsync(string userId, UserRole role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        return await _userManager.IsInRoleAsync(user, role.ToString());
    }
} 