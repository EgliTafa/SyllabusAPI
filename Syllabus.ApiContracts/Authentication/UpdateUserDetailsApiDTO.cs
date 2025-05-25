namespace Syllabus.ApiContracts.Authentication
{
    public class UpdateUserDetailsApiDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhonePrefix { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
    }
} 