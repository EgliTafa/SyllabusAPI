using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.ChangePassword
{
    public record ChangePasswordCommand(ChangePasswordRequestApiDTO Request) : IRequest<ErrorOr<ChangePasswordResponseApiDTO>>;

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<ChangePasswordResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public ChangePasswordCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<ChangePasswordResponseApiDTO>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return Error.NotFound("User not found.");

            // Verify current password
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!isCurrentPasswordValid)
                return Error.Validation("Current password is incorrect.");

            // Validate new password
            if (request.NewPassword != request.ConfirmPassword)
                return Error.Validation("New password and confirmation password do not match.");

            // Change password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return Error.Validation(string.Join("; ", result.Errors.Select(e => e.Description)));

            return new ChangePasswordResponseApiDTO
            {
                Message = "Password changed successfully",
                ChangedAt = DateTime.UtcNow
            };
        }
    }
} 