using System.IO.Pipelines;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Authentication;
using Syllabus.Domain.Services.Email;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.Register
{
    public record RegisterUserCommand(RegisterUserRequestApiDTO Request) : IRequest<ErrorOr<RegisterUserResponseApiDTO>>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<RegisterUserResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IBrevoEmailService _emailService;


        public RegisterUserCommandHandler(UserManager<UserEntity> userManager, IJwtTokenGenerator jwtTokenGenerator, IBrevoEmailService emailService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public async Task<ErrorOr<RegisterUserResponseApiDTO>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Error.Conflict("User with this email already exists.");
            }

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
                Status = UserStatus.Active
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return Error.Validation(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var sendEmailResult = await _emailService.SendEmailConfirmationAsync(user.Email!, emailConfirmationToken);

            if (!sendEmailResult)
            {
                return Error.Failure("Failed to send email confirmation.");
            }

            var token = await _jwtTokenGenerator.GenerateTokenAsync(user);

            return new RegisterUserResponseApiDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };
        }
    }
}
