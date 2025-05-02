/// <summary>
/// Request model for creating or updating course topics.
/// </summary>
public class TopicRequestApiDTO
{
    /// <summary>
    /// The title of the topic.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// The number of hours allocated for this topic.
    /// </summary>
    public int Hours { get; set; }

    /// <summary>
    /// Optional reference materials for this topic.
    /// </summary>
    public string? Reference { get; set; }
}