using System.IO.Pipelines;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Authentication;
using Syllabus.Domain.Services.Email;
using Syllabus.Domain.Users;
using Syllabus.Infrastructure.Data;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.Register
{
    public record RegisterUserCommand(RegisterUserRequestApiDTO Request) : IRequest<ErrorOr<RegisterUserResponseApiDTO>>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<RegisterUserResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IBrevoEmailService _emailService;
        private readonly SyllabusDbContext _dbContext;

        public RegisterUserCommandHandler(UserManager<UserEntity> userManager, IJwtTokenGenerator jwtTokenGenerator, IBrevoEmailService emailService, SyllabusDbContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ErrorOr<RegisterUserResponseApiDTO>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            
            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.Email))
                return AuthenticationErrors.EmailRequired;

            if (string.IsNullOrWhiteSpace(request.Password))
                return AuthenticationErrors.PasswordRequired;

            if (string.IsNullOrWhiteSpace(request.FirstName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(request.LastName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(request.PhonePrefix))
                return AuthenticationErrors.PhoneNumberRequired;

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                return AuthenticationErrors.PhoneNumberRequired;

            // Validate email format
            if (!EmailValidator.IsValid(request.Email))
                return AuthenticationErrors.InvalidEmailFormat;

            // Check if user already exists by email
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return AuthenticationErrors.EmailAlreadyExists;

            // Check if phone number already exists
            var existingPhoneUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => 
                    u.PhoneNumberInfo.Prefix == request.PhonePrefix && 
                    u.PhoneNumberInfo.Number == request.PhoneNumber, 
                    cancellationToken);
            
            if (existingPhoneUser != null)
                return AuthenticationErrors.PhoneNumberAlreadyExists;

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
                return AuthenticationErrors.UserCreationFailed;

            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var sendEmailResult = await _emailService.SendEmailConfirmationAsync(user.Email!, emailConfirmationToken);

            if (!sendEmailResult)
                return AuthenticationErrors.EmailConfirmationFailed;

            var token = await _jwtTokenGenerator.GenerateTokenAsync(user);

            return new RegisterUserResponseApiDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token,
                EmailConfirmed = user.EmailConfirmed
            };
        }
    }
}
