using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

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
                return AuthenticationErrors.MissingRequiredFields;
            }

            if (!EmailValidator.IsValid(request.Email))
            {
                return AuthenticationErrors.InvalidEmailFormat;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return AuthenticationErrors.UserByEmailNotFound;
            }

            var resetResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

            if (!resetResult.Succeeded)
            {
                return AuthenticationErrors.UserUpdateFailed;
            }

            return new ResetPasswordResponseApiDTO { Message = "Password has been successfully reset." };
        }
    }
}
