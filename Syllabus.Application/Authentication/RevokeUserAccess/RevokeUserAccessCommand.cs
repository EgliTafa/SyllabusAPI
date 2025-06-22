using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.RevokeUserAccess
{
    public record RevokeUserAccessCommand(string UserId, RevokeUserAccessRequestApiDTO Request) : IRequest<ErrorOr<RevokeUserAccessResponseApiDTO>>;

    public class RevokeUserAccessCommandHandler : IRequestHandler<RevokeUserAccessCommand, ErrorOr<RevokeUserAccessResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public RevokeUserAccessCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<RevokeUserAccessResponseApiDTO>> Handle(RevokeUserAccessCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return AuthenticationErrors.UserByIdNotFound;
            }

            // Calculate lockout end date
            DateTimeOffset lockoutEnd;
            if (command.Request.LockoutDurationDays.HasValue && command.Request.LockoutDurationDays.Value > 0)
            {
                lockoutEnd = DateTimeOffset.UtcNow.AddDays(command.Request.LockoutDurationDays.Value);
            }
            else
            {
                // Permanent lockout (set to a far future date)
                lockoutEnd = DateTimeOffset.MaxValue;
            }

            // Enable lockout and set lockout end
            user.LockoutEnabled = true;
            user.LockoutEnd = lockoutEnd;
            user.Status = UserStatus.Suspended;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return AuthenticationErrors.LockoutFailed;
            }

            return new RevokeUserAccessResponseApiDTO
            {
                Id = user.Id,
                Email = user.Email,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                Status = user.Status.ToString(),
                Message = command.Request.LockoutDurationDays.HasValue 
                    ? $"User access revoked for {command.Request.LockoutDurationDays.Value} days."
                    : "User access permanently revoked."
            };
        }
    }
} 