namespace Syllabus.Util.Options
{
    public class ApplicationOptions
    {
        public static string SectionName => "Application";

        public required string Name { get; set; }

        public required string Version { get; set; }

        public required string InformationalVersion { get; set; }
    }
}
