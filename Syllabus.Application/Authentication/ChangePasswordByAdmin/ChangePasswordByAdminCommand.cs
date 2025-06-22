using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.ChangePasswordByAdmin
{
    public record ChangePasswordByAdminCommand(string UserId, ChangePasswordByAdminRequestApiDTO Request) : IRequest<ErrorOr<ChangePasswordByAdminResponseApiDTO>>;

    public class ChangePasswordByAdminCommandHandler : IRequestHandler<ChangePasswordByAdminCommand, ErrorOr<ChangePasswordByAdminResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public ChangePasswordByAdminCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<ChangePasswordByAdminResponseApiDTO>> Handle(ChangePasswordByAdminCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var userId = command.UserId;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.NewPassword))
                return AuthenticationErrors.PasswordRequired;

            if (string.IsNullOrWhiteSpace(request.ConfirmPassword))
                return AuthenticationErrors.PasswordRequired;

            // Check if passwords match
            if (request.NewPassword != request.ConfirmPassword)
                return AuthenticationErrors.PasswordsDoNotMatch;

            // Validate password strength
            if (request.NewPassword.Length < 6)
                return AuthenticationErrors.PasswordTooShort;

            // Find the user
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return AuthenticationErrors.UserNotFound;

            // Generate password reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Reset the password
            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return AuthenticationErrors.PasswordChangeFailed;
            }

            return new ChangePasswordByAdminResponseApiDTO
            {
                Message = "Password changed successfully",
                ChangedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
            };
        }
    }
} 