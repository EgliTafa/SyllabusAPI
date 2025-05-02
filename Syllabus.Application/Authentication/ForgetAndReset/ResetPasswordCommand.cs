using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.ForgetAndReset
{
    public record ResetPasswordCommand(ResetPasswordApiDTO Request) : IRequest<ErrorOr<ResetPasswordResponseApiDTO>>;

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<ResetPasswordResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public ResetPasswordCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<ResetPasswordResponseApiDTO>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Token) || string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return Error.Validation("Email, token, and new password are required.");
            }

            if (!EmailValidator.IsValid(request.Email))
            {
                return Error.Validation("Invalid email format.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Error.NotFound("User not found.");
            }

            var resetResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!resetResult.Succeeded)
            {
                var errors = resetResult.Errors.Select(e => e.Description).ToList();
                return Error.Validation(string.Join(" | ", errors));
            }

            return new ResetPasswordResponseApiDTO { Message = "Password has been successfully reset." };
        }
    }
}
