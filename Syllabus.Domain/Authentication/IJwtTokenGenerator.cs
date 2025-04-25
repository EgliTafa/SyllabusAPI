using Syllabus.Domain.Users;

namespace Syllabus.Domain.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserEntity user);
    }
}
