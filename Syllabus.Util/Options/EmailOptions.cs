namespace Syllabus.Util.Options
{
    public class EmailOptions
    {
        public const string SectionName = "EmailSettings";
        
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string ResetPasswordUrl { get; set; }
        public int DefaultListId { get; set; }
        public string BaseUrl { get; set; } = "https://api.brevo.com/v3/";
    }
} 