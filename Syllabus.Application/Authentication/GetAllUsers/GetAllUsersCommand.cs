using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.GetAllUsers
{
    public record GetAllUsersCommand : IRequest<ErrorOr<List<GetAllUsersResponseApiDTO>>>;

    public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, ErrorOr<List<GetAllUsersResponseApiDTO>>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public GetAllUsersCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<List<GetAllUsersResponseApiDTO>>> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<GetAllUsersResponseApiDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                
                userDtos.Add(new GetAllUsersResponseApiDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = $"{user.PhoneNumberInfo?.Prefix}{user.PhoneNumberInfo?.Number}",
                    EmailConfirmed = user.EmailConfirmed,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEnd = user.LockoutEnd,
                    Status = user.Status.ToString(),
                    Roles = roles.ToList(),
                    ProfilePictureUrl = user.ProfilePictureUrl
                });
            }

            return userDtos;
        }
    }
} 