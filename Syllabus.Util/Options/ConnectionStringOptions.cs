namespace Syllabus.Util.Options
{
    public class ConnectionStringOptions
    {
        public static string SectionName => "ConnectionStrings";
        public string? DefaultConnection { get; set; }
    }
}
