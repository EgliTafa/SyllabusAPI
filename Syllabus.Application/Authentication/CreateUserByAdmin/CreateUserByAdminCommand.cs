using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Infrastructure.Data;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.CreateUserByAdmin
{
    public record CreateUserByAdminCommand(CreateUserByAdminRequestApiDTO Request) : IRequest<ErrorOr<CreateUserByAdminResponseApiDTO>>;

    public class CreateUserByAdminCommandHandler : IRequestHandler<CreateUserByAdminCommand, ErrorOr<CreateUserByAdminResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SyllabusDbContext _dbContext;

        public CreateUserByAdminCommandHandler(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, SyllabusDbContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ErrorOr<CreateUserByAdminResponseApiDTO>> Handle(CreateUserByAdminCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.Email))
                return AuthenticationErrors.EmailRequired;

            if (string.IsNullOrWhiteSpace(request.Password))
                return AuthenticationErrors.PasswordRequired;

            if (string.IsNullOrWhiteSpace(request.FirstName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(request.LastName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(request.PhonePrefix))
                return AuthenticationErrors.PhoneNumberRequired;

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                return AuthenticationErrors.PhoneNumberRequired;

            // Validate email format
            if (!EmailValidator.IsValid(request.Email))
                return AuthenticationErrors.InvalidEmailFormat;

            // Check if user already exists by email
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return AuthenticationErrors.EmailAlreadyExists;

            // Check if phone number already exists
            var existingPhoneUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => 
                    u.PhoneNumberInfo.Prefix == request.PhonePrefix && 
                    u.PhoneNumberInfo.Number == request.PhoneNumber, 
                    cancellationToken);
            
            if (existingPhoneUser != null)
                return AuthenticationErrors.PhoneNumberAlreadyExists;

            // Validate role
            if (!Enum.TryParse<UserRole>(request.Role, true, out var userRole))
                return AuthenticationErrors.InvalidRole;

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
                return AuthenticationErrors.UserCreationFailed;

            // Ensure role exists
            var roleName = userRole.ToString();
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleCreationResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleCreationResult.Succeeded)
                    return AuthenticationErrors.RoleAssignmentFailed;
            }

            // Assign role to user
            var roleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleResult.Succeeded)
            {
                // If role assignment fails, delete the user and return error
                await _userManager.DeleteAsync(user);
                return AuthenticationErrors.RoleAssignmentFailed;
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