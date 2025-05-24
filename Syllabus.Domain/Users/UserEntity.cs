using Microsoft.AspNetCore.Identity;

namespace Syllabus.Domain.Users
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Email is inherited from IdentityUser
        public bool EmailVerified { get; set; }

        public PhoneNumber PhoneNumberInfo { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public UserStatus Status { get; set; } = UserStatus.Active;
    }
}
