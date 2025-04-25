namespace Syllabus.ApiContracts.Authentication
{
    public class LoginRequestApiDTO
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
