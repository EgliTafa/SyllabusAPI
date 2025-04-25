using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.Register
{
    public record RegisterUserCommand(RegisterUserRequestApiDTO Request) : IRequest<ErrorOr<RegisterUserResponseApiDTO>>;

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ErrorOr<RegisterUserResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterUserCommandHandler(
            UserManager<UserEntity> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<RegisterUserResponseApiDTO>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Error.Conflict(description: $"User with email {request.Email} already exists.");
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
                return Error.Validation(description: string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

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
