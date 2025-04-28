using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Services.Email;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.ForgetAndReset
{
    public record ForgotPasswordCommand(ForgotPasswordApiDTO Request) : IRequest<ErrorOr<ForgotPasswordResponseApiDTO>>;

    public class ForgotPasswordCommandHandler
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IBrevoEmailService _emailService;

        public ForgotPasswordCommandHandler(UserManager<UserEntity> userManager, IBrevoEmailService emailService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task<ErrorOr<ForgotPasswordResponseApiDTO>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return Error.Validation("Email is required.");
            }

            if (!EmailValidator.IsValid(request.Email))
            {
                return Error.Validation("Invalid email format.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ForgotPasswordResponseApiDTO { Message = "If the email exists, a password reset link has been sent." };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailService.SendPasswordResetEmailAsync(user.Email!, token);

            return new ForgotPasswordResponseApiDTO { Message = "If the email exists, a password reset link has been sent." };
        }
    }
}
