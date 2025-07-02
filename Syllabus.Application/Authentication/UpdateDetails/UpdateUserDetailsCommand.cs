using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;
using Syllabus.Infrastructure.Data;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.UpdateDetails
{
    public record UpdateUserDetailsCommand(string UserId, UpdateUserDetailsApiDTO Request) : IRequest<ErrorOr<UpdateUserDetailsResponseApiDTO>>;

    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, ErrorOr<UpdateUserDetailsResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SyllabusDbContext _dbContext;

        public UpdateUserDetailsCommandHandler(UserManager<UserEntity> userManager, SyllabusDbContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ErrorOr<UpdateUserDetailsResponseApiDTO>> Handle(UpdateUserDetailsCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
                return AuthenticationErrors.UserByIdNotFound;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(command.Request.FirstName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(command.Request.LastName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(command.Request.PhonePrefix))
                return AuthenticationErrors.PhoneNumberRequired;

            if (string.IsNullOrWhiteSpace(command.Request.PhoneNumber))
                return AuthenticationErrors.PhoneNumberRequired;

            // Check if phone number is being changed and if it's already taken by another user
            var currentPhoneKey = $"{user.PhoneNumberInfo.Prefix}_{user.PhoneNumberInfo.Number}";
            var newPhoneKey = $"{command.Request.PhonePrefix}_{command.Request.PhoneNumber}";
            
            if (currentPhoneKey != newPhoneKey)
            {
                var existingPhoneUser = await _dbContext.Users
                    .FirstOrDefaultAsync(u => 
                        u.Id != command.UserId &&
                        u.PhoneNumberInfo.Prefix == command.Request.PhonePrefix && 
                        u.PhoneNumberInfo.Number == command.Request.PhoneNumber, 
                        cancellationToken);
                
                if (existingPhoneUser != null)
                    return AuthenticationErrors.PhoneNumberAlreadyExists;
            }

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
                return AuthenticationErrors.UserUpdateFailed;

            return new UpdateUserDetailsResponseApiDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhonePrefix = user.PhoneNumberInfo.Prefix,
                PhoneNumber = user.PhoneNumberInfo.Number,
                ProfilePictureUrl = user.ProfilePictureUrl,
                EmailConfirmed = user.EmailConfirmed
            };
        }
    }
} 