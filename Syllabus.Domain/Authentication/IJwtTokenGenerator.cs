using Syllabus.Domain.Users;

namespace Syllabus.Domain.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(UserEntity user);
    }
}
