using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.CreateUserByAdmin
{
    public record CreateUserByAdminCommand(CreateUserByAdminRequestApiDTO Request) : IRequest<ErrorOr<CreateUserByAdminResponseApiDTO>>;

    public class CreateUserByAdminCommandHandler : IRequestHandler<CreateUserByAdminCommand, ErrorOr<CreateUserByAdminResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUserByAdminCommandHandler(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<ErrorOr<CreateUserByAdminResponseApiDTO>> Handle(CreateUserByAdminCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Error.Conflict("User with this email already exists.");
            }

            // Validate role
            if (!Enum.TryParse<UserRole>(request.Role, true, out var userRole))
            {
                return Error.Validation($"Invalid role: {request.Role}. Valid roles are: {string.Join(", ", Enum.GetNames<UserRole>())}");
            }

            // Create user
            var user = new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                PhoneNumberInfo = new PhoneNumber
                {
                    Prefix = request.PhonePrefix,
                    Number = request.PhoneNumber
                },
                ProfilePictureUrl = request.ProfilePictureUrl ?? string.Empty,
                Status = UserStatus.Active,
                EmailConfirmed = true, // Admin-created users don't need email confirmation
                LockoutEnabled = false
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return Error.Validation(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            // Ensure role exists
            var roleName = userRole.ToString();
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Assign role to user
            var roleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleResult.Succeeded)
            {
                // If role assignment fails, delete the user and return error
                await _userManager.DeleteAsync(user);
                return Error.Failure("Failed to assign role to user.");
            }

            return new CreateUserByAdminResponseApiDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = roleName,
                Status = user.Status.ToString()
            };
        }
    }
} 