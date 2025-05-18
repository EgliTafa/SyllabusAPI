using Syllabus.Domain.Users;

namespace Syllabus.Domain.Authentication
{
    public interface IRoleManagementService
    {
        Task<bool> AssignRoleToUserAsync(string userId, UserRole role);
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        Task<bool> IsUserInRoleAsync(string userId, UserRole role);
        Task<bool> RemoveRoleFromUserAsync(string userId, UserRole role);
    }
}