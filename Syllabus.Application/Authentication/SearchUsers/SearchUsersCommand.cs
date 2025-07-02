using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Domain.Users;

namespace Syllabus.Application.Authentication.SearchUsers
{
    public record SearchUsersCommand(string Query) : IRequest<ErrorOr<List<SearchUsersResponseApiDTO>>>;

    public class SearchUsersCommandHandler : IRequestHandler<SearchUsersCommand, ErrorOr<List<SearchUsersResponseApiDTO>>>
    {
        private readonly UserManager<UserEntity> _userManager;

        public SearchUsersCommandHandler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ErrorOr<List<SearchUsersResponseApiDTO>>> Handle(SearchUsersCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Query) || command.Query.Length < 2)
            {
                return new List<SearchUsersResponseApiDTO>();
            }

            var query = command.Query.ToLower();
            var users = await _userManager.Users
                .Where(u => 
                    u.Email.ToLower().Contains(query) ||
                    u.FirstName.ToLower().Contains(query) ||
                    u.LastName.ToLower().Contains(query) ||
                    (u.FirstName + " " + u.LastName).ToLower().Contains(query))
                .Take(10) // Limit results to 10 for performance
                .ToListAsync(cancellationToken);

            var userDtos = users.Select(user => new SearchUsersResponseApiDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            }).ToList();

            return userDtos;
        }
    }
} 