using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.UpdateUserByAdmin
{
    public record UpdateUserByAdminCommand(string UserId, UpdateUserByAdminRequestApiDTO Request) : IRequest<ErrorOr<UpdateUserByAdminResponseApiDTO>>;

    public class UpdateUserByAdminCommandHandler : IRequestHandler<UpdateUserByAdminCommand, ErrorOr<UpdateUserByAdminResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateUserByAdminCommandHandler(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<ErrorOr<UpdateUserByAdminResponseApiDTO>> Handle(UpdateUserByAdminCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
                return Error.NotFound("User not found.");

            // Check if email is being changed and if it's already taken
            if (user.Email != command.Request.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(command.Request.Email);
                if (existingUser != null && existingUser.Id != command.UserId)
                    return Error.Conflict("Email is already taken.");
            }

            // Update user details
            user.FirstName = command.Request.FirstName;
            user.LastName = command.Request.LastName;
            user.Email = command.Request.Email;
            user.UserName = command.Request.Email; // Keep username in sync with email
            user.PhoneNumberInfo = new PhoneNumber
            {
                Prefix = command.Request.PhonePrefix,
                Number = command.Request.PhoneNumber
            };
            user.ProfilePictureUrl = command.Request.ProfilePictureUrl;

            // Update user
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return Error.Validation(string.Join("; ", updateResult.Errors.Select(e => e.Description)));

            // Update email if changed
            if (user.Email != command.Request.Email)
            {
                var emailChangeResult = await _userManager.SetEmailAsync(user, command.Request.Email);
                if (!emailChangeResult.Succeeded)
                    return Error.Validation(string.Join("; ", emailChangeResult.Errors.Select(e => e.Description)));
            }

            // Update role
            var currentRoles = await _userManager.GetRolesAsync(user);
            var newRole = command.Request.Role;

            // Remove current roles
            if (currentRoles.Any())
            {
                var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeRolesResult.Succeeded)
                    return Error.Validation(string.Join("; ", removeRolesResult.Errors.Select(e => e.Description)));
            }

            // Add new role
            var addRoleResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addRoleResult.Succeeded)
                return Error.Validation(string.Join("; ", addRoleResult.Errors.Select(e => e.Description)));

            return new UpdateUserByAdminResponseApiDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhonePrefix = user.PhoneNumberInfo.Prefix,
                PhoneNumber = user.PhoneNumberInfo.Number,
                Role = newRole,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Status = user.LockoutEnabled && user.LockoutEnd > DateTime.UtcNow ? "Locked" : "Active",
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd?.ToString("yyyy-MM-ddTHH:mm:ssZ")
            };
        }
    }
} 