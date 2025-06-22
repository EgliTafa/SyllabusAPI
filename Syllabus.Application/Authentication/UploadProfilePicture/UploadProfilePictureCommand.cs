using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Services.File;
using Syllabus.Domain.Users;
using Syllabus.Application.Authentication;

namespace Syllabus.Application.Authentication.UploadProfilePicture
{
    public record UploadProfilePictureCommand(string UserId, UploadProfilePictureRequestApiDTO Request) : IRequest<ErrorOr<UploadProfilePictureResponseApiDTO>>;

    public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand, ErrorOr<UploadProfilePictureResponseApiDTO>>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IFileService _fileService;

        public UploadProfilePictureCommandHandler(UserManager<UserEntity> userManager, IFileService fileService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public async Task<ErrorOr<UploadProfilePictureResponseApiDTO>> Handle(UploadProfilePictureCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            if (user == null)
                return AuthenticationErrors.UserByIdNotFound;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(command.Request.File))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(command.Request.FileName))
                return AuthenticationErrors.MissingRequiredFields;

            if (string.IsNullOrWhiteSpace(command.Request.ContentType))
                return AuthenticationErrors.MissingRequiredFields;

            // Validate file type
            if (!command.Request.ContentType.StartsWith("image/"))
                return AuthenticationErrors.InvalidFileFormat;

            // Delete old profile picture if it exists
            if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
            {
                var deleteResult = await _fileService.DeleteProfilePictureAsync(user.ProfilePictureUrl);
                if (!deleteResult)
                    return AuthenticationErrors.ProfilePictureUploadFailed;
            }

            // Upload new profile picture
            var newProfilePictureUrl = await _fileService.UploadProfilePictureAsync(
                command.Request.File,
                command.Request.FileName,
                command.Request.ContentType,
                command.UserId
            );

            if (string.IsNullOrEmpty(newProfilePictureUrl))
                return AuthenticationErrors.ProfilePictureUploadFailed;

            // Update user's profile picture URL
            user.ProfilePictureUrl = newProfilePictureUrl;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                return AuthenticationErrors.UserUpdateFailed;

            return new UploadProfilePictureResponseApiDTO
            {
                ProfilePictureUrl = newProfilePictureUrl,
                Message = "Profile picture uploaded successfully."
            };
        }
    }
} 