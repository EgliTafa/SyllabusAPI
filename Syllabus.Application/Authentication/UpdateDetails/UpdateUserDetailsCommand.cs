using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.UpdateDetails
{
    public record UpdateUserDetailsCommand(string UserId, UpdateUserDetailsApiDTO Request) : IRequest<ErrorOr<UpdateUserDetailsResponseApiDTO>>;

    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, ErrorOr<UpdateUserDetailsResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public UpdateUserDetailsCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<UpdateUserDetailsResponseApiDTO>> Handle(UpdateUserDetailsCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
                return Error.NotFound("User not found.");

            user.FirstName = command.Request.FirstName;
            user.LastName = command.Request.LastName;
            user.PhoneNumberInfo = new PhoneNumber
            {
                Prefix = command.Request.PhonePrefix,
                Number = command.Request.PhoneNumber
            };
            user.ProfilePictureUrl = command.Request.ProfilePictureUrl;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Error.Validation(string.Join("; ", result.Errors.Select(e => e.Description)));

            return new UpdateUserDetailsResponseApiDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhonePrefix = user.PhoneNumberInfo.Prefix,
                PhoneNumber = user.PhoneNumberInfo.Number,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }
    }
} 