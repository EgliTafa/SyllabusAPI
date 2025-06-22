using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Infrastructure.Data;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.UpdateUserByAdmin
{
    public record UpdateUserByAdminCommand(string UserId, UpdateUserByAdminRequestApiDTO Request) : IRequest<ErrorOr<UpdateUserByAdminResponseApiDTO>>;

    public class UpdateUserByAdminCommandHandler : IRequestHandler<UpdateUserByAdminCommand, ErrorOr<UpdateUserByAdminResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SyllabusDbContext _dbContext;

        public UpdateUserByAdminCommandHandler(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, SyllabusDbContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ErrorOr<UpdateUserByAdminResponseApiDTO>> Handle(UpdateUserByAdminCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
                return AuthenticationErrors.UserByIdNotFound;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(command.Request.Email))
                return AuthenticationErrors.EmailRequired;

            if (string.IsNullOrWhiteSpace(command.Request.FirstName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(command.Request.LastName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(command.Request.PhonePrefix))
                return AuthenticationErrors.PhoneNumberRequired;

            if (string.IsNullOrWhiteSpace(command.Request.PhoneNumber))
                return AuthenticationErrors.PhoneNumberRequired;

            if (string.IsNullOrWhiteSpace(command.Request.Role))
                return AuthenticationErrors.InvalidRole;

            // Check if email is being changed and if it's already taken
            if (user.Email != command.Request.Email)
            {
                var existingUser = await _userManager.FindByEmailAsync(command.Request.Email);
                if (existingUser != null && existingUser.Id != command.UserId)
                    return AuthenticationErrors.EmailAlreadyTaken;
            }

            // Check if phone number is being changed and if it's already taken by another user
            var currentPhoneKey = $"{user.PhoneNumberInfo.Prefix}_{user.PhoneNumberInfo.Number}";
            var newPhoneKey = $"{command.Request.PhonePrefix}_{command.Request.PhoneNumber}";
            
            if (currentPhoneKey != newPhoneKey)
            {
                var existingPhoneUser = await _dbContext.Users
                    .FirstOrDefaultAsync(u => 
                        u.Id != command.UserId &&
                        u.PhoneNumberInfo.Prefix == command.Request.PhonePrefix && 
                        u.PhoneNumberInfo.Number == command.Request.PhoneNumber, 
                        cancellationToken);
                
                if (existingPhoneUser != null)
                    return AuthenticationErrors.PhoneNumberAlreadyExists;
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
                return AuthenticationErrors.UserUpdateFailed;

            // Update email if changed
            if (user.Email != command.Request.Email)
            {
                var emailChangeResult = await _userManager.SetEmailAsync(user, command.Request.Email);
                if (!emailChangeResult.Succeeded)
                    return AuthenticationErrors.UserUpdateFailed;
            }

            // Update role
            var currentRoles = await _userManager.GetRolesAsync(user);
            var newRole = command.Request.Role;

            // Remove current roles
            if (currentRoles.Any())
            {
                var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeRolesResult.Succeeded)
                    return AuthenticationErrors.RoleRemovalFailed;
            }

            // Add new role
            var addRoleResult = await _userManager.AddToRoleAsync(user, newRole);
            if (!addRoleResult.Succeeded)
                return AuthenticationErrors.RoleAssignmentFailed;

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