using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.RestoreUserAccess
{
    public record RestoreUserAccessCommand(string UserId) : IRequest<ErrorOr<RestoreUserAccessResponseApiDTO>>;

    public class RestoreUserAccessCommandHandler : IRequestHandler<RestoreUserAccessCommand, ErrorOr<RestoreUserAccessResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public RestoreUserAccessCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<RestoreUserAccessResponseApiDTO>> Handle(RestoreUserAccessCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return AuthenticationErrors.UserByIdNotFound;
            }

            // Remove lockout
            user.LockoutEnabled = false;
            user.LockoutEnd = null;
            user.Status = UserStatus.Active;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return AuthenticationErrors.UnlockFailed;
            }

            return new RestoreUserAccessResponseApiDTO
            {
                Id = user.Id,
                Email = user.Email,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                Status = user.Status.ToString(),
                Message = "User access restored successfully."
            };
        }
    }
} 