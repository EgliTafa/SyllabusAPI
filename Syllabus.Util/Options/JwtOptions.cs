namespace Syllabus.Util.Options
{
    public class JwtServiceOptions
    {
        public const string Key = "Jwt";
        public JwtOptions Jwt { get; set; }
    }

    public class JwtOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

    }
}
