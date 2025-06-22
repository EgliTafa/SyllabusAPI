using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.DeleteUser
{
    public record DeleteUserCommand(string UserId) : IRequest<ErrorOr<bool>>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<bool>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public DeleteUserCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return AuthenticationErrors.UserByIdNotFound;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return AuthenticationErrors.UserDeletionFailed;
            }

            return true;
        }
    }
} 