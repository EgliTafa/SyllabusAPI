using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Services.Email;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.ResendEmailConfirmation
{
    public record ResendEmailConfirmationCommand(ResendEmailConfirmationApiDTO Request) : IRequest<ErrorOr<ResendEmailConfirmationResponseApiDTO>>;

    public class ResendEmailConfirmationCommandHandler : IRequestHandler<ResendEmailConfirmationCommand, ErrorOr<ResendEmailConfirmationResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IBrevoEmailService _emailService;

        public ResendEmailConfirmationCommandHandler(UserManager<UserEntity> userManager, IBrevoEmailService emailService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task<ErrorOr<ResendEmailConfirmationResponseApiDTO>> Handle(ResendEmailConfirmationCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return AuthenticationErrors.EmailRequired;
            }

            if (!EmailValidator.IsValid(request.Email))
            {
                return AuthenticationErrors.InvalidEmailFormat;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ResendEmailConfirmationResponseApiDTO { Message = "If the email exists, a confirmation email has been sent." };
            }

            if (user.EmailConfirmed)
            {
                return new ResendEmailConfirmationResponseApiDTO { Message = "Email is already confirmed." };
            }

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var sendEmailResult = await _emailService.SendEmailConfirmationAsync(user.Email!, emailConfirmationToken);

            if (!sendEmailResult)
            {
                return AuthenticationErrors.EmailConfirmationFailed;
            }

            return new ResendEmailConfirmationResponseApiDTO { Message = "Email confirmation has been sent." };
        }
    }
} 