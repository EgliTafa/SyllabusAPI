using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Services.File;
using Syllabus.Domain.Users;

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
                return Error.NotFound("User not found.");

            try
            {
                // Delete old profile picture if it exists
                if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                {
                    await _fileService.DeleteProfilePictureAsync(user.ProfilePictureUrl);
                }

                // Upload new profile picture
                var newProfilePictureUrl = await _fileService.UploadProfilePictureAsync(
                    command.Request.File,
                    command.Request.FileName,
                    command.Request.ContentType,
                    command.UserId
                );

                // Update user's profile picture URL
                user.ProfilePictureUrl = newProfilePictureUrl;
                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                {
                    return Error.Validation(string.Join("; ", updateResult.Errors.Select(e => e.Description)));
                }

                return new UploadProfilePictureResponseApiDTO
                {
                    ProfilePictureUrl = newProfilePictureUrl,
                    Message = "Profile picture uploaded successfully."
                };
            }
            catch (ArgumentException ex)
            {
                return Error.Validation(ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure($"Failed to upload profile picture: {ex.Message}");
            }
        }
    }
} 