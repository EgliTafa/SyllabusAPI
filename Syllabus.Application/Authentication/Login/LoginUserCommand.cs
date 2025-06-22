using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.Login
{
    public record LoginUserCommand(LoginRequestApiDTO Request) : IRequest<ErrorOr<LoginResponseApiDTO>>;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ErrorOr<LoginResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCommandHandler(IJwtTokenGenerator jwtTokenGenerator, UserManager<UserEntity> userManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<LoginResponseApiDTO>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(request.Email))
                return AuthenticationErrors.EmailRequired;

            if (string.IsNullOrWhiteSpace(request.Password))
                return AuthenticationErrors.PasswordRequired;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return AuthenticationErrors.Unauthorized;
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return AuthenticationErrors.Unauthorized;
            }

            var token = await _jwtTokenGenerator.GenerateTokenAsync(user);

            return new LoginResponseApiDTO
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
