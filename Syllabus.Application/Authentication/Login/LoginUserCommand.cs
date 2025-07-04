using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.Login
{
    public record LoginUserCommand(LoginRequestApiDTO Request) : IRequest<ErrorOr<LoginResponseApiDTO>>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ErrorOr<LoginResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommandHandler(IJwtTokenGenerator jwtTokenGenerator, UserManager<UserEntity> userManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<LoginResponseApiDTO>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.Email))
                return AuthenticationErrors.EmailRequired;

            if (string.IsNullOrWhiteSpace(request.Password))
                return AuthenticationErrors.PasswordRequired;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return AuthenticationErrors.Unauthorized;
            }

            // Check if user is an admin and unlock them if they're locked out
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains("Administrator") && user.LockoutEnabled)
            {
                // Unlock admin user
                user.LockoutEnabled = false;
                user.LockoutEnd = null;
                user.Status = UserStatus.Active;
                await _userManager.UpdateAsync(user);
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return AuthenticationErrors.Unauthorized;
            }

            var token = await _jwtTokenGenerator.GenerateTokenAsync(user);

            // Determine the correct status based on lockout state
            string status;
            if (user.LockoutEnabled)
            {
                if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
                {
                    status = "Locked"; // Temporarily locked
                }
                else if (!user.LockoutEnd.HasValue)
                {
                    status = "Locked"; // Permanently locked
                }
                else
                {
                    status = "Active"; // Lockout expired
                }
            }
            else
            {
                status = user.Status.ToString();
            }

            return new LoginResponseApiDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token,
                EmailConfirmed = user.EmailConfirmed,
                ProfilePictureUrl = user.ProfilePictureUrl,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                Status = status,
                LockoutReason = user.LockoutReason
            };
        }
    }
}
