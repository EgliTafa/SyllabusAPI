using System.Text.Json.Serialization;

namespace Syllabus.ApiContracts.Authentication
{
    public class UpdateUserByAdminResponseApiDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;
        
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;
        
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        
        [JsonPropertyName("phonePrefix")]
        public string PhonePrefix { get; set; } = string.Empty;
        
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;
        
        [JsonPropertyName("profilePictureUrl")]
        public string? ProfilePictureUrl { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        
        [JsonPropertyName("emailConfirmed")]
        public bool EmailConfirmed { get; set; }
        
        [JsonPropertyName("lockoutEnabled")]
        public bool LockoutEnabled { get; set; }
        
        [JsonPropertyName("lockoutEnd")]
        public string? LockoutEnd { get; set; }
    }
} 