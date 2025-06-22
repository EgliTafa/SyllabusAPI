using ErrorOr;

namespace Syllabus.Application.Courses;

public static partial class CourseErrors
{
    // Course not found errors
    public static readonly Error CourseNotFound = Error.NotFound("Course.NotFound", "Course not found.");
    public static readonly Error CourseByIdNotFound = Error.NotFound("Course.ByIdNotFound", "Course with the given ID was not found.");
    public static readonly Error CourseByCodeNotFound = Error.NotFound("Course.ByCodeNotFound", "Course with the given code was not found.");

    // Course creation errors
    public static readonly Error CourseCreationFailed = Error.Failure("Course.CreationFailed", "Failed to create course.");
    public static readonly Error CourseAlreadyExists = Error.Conflict("Course.AlreadyExists", "Course with this code already exists.");
    public static readonly Error InvalidCourseData = Error.Validation("Course.InvalidData", "Invalid course data provided.");

    // Course update errors
    public static readonly Error CourseUpdateFailed = Error.Failure("Course.UpdateFailed", "Failed to update course.");
    public static readonly Error CourseUpdateNoChanges = Error.Validation("Course.UpdateNoChanges", "No changes were made to the course.");

    // Course deletion errors
    public static readonly Error CourseDeletionFailed = Error.Failure("Course.DeletionFailed", "Failed to delete course.");
    public static readonly Error CourseInUse = Error.Conflict("Course.InUse", "Cannot delete course that is being used in syllabi.");

    // Course detail errors
    public static readonly Error CourseDetailNotFound = Error.NotFound("Course.DetailNotFound", "Course detail not found.");
    public static readonly Error CourseDetailAlreadyExists = Error.Conflict("Course.DetailAlreadyExists", "Course detail already exists.");
    public static readonly Error CourseDetailCreationFailed = Error.Failure("Course.DetailCreationFailed", "Failed to create course detail.");
    public static readonly Error CourseDetailUpdateFailed = Error.Failure("Course.DetailUpdateFailed", "Failed to update course detail.");
    public static readonly Error CourseDetailDeletionFailed = Error.Failure("Course.DetailDeletionFailed", "Failed to delete course detail.");

    // Topic errors
    public static readonly Error TopicNotFound = Error.NotFound("Course.TopicNotFound", "Topic not found.");
    public static readonly Error TopicCreationFailed = Error.Failure("Course.TopicCreationFailed", "Failed to create topic.");
    public static readonly Error TopicUpdateFailed = Error.Failure("Course.TopicUpdateFailed", "Failed to update topic.");
    public static readonly Error TopicDeletionFailed = Error.Failure("Course.TopicDeletionFailed", "Failed to delete topic.");

    // Validation errors
    public static readonly Error InvalidCourseCode = Error.Validation("Course.InvalidCode", "Invalid course code.");
    public static readonly Error InvalidCourseName = Error.Validation("Course.InvalidName", "Invalid course name.");
    public static readonly Error InvalidCourseDescription = Error.Validation("Course.InvalidDescription", "Invalid course description.");
    public static readonly Error InvalidCredits = Error.Validation("Course.InvalidCredits", "Invalid number of credits.");
    public static readonly Error InvalidHours = Error.Validation("Course.InvalidHours", "Invalid number of hours.");
    public static readonly Error InvalidCourseType = Error.Validation("Course.InvalidType", "Invalid course type.");
    public static readonly Error MissingRequiredFields = Error.Validation("Course.MissingRequiredFields", "Required course fields are missing.");

    // Export errors
    public static readonly Error ExportFailed = Error.Failure("Course.ExportFailed", "Failed to export course.");
    public static readonly Error ExportFormatNotSupported = Error.Validation("Course.ExportFormatNotSupported", "Export format is not supported.");
    public static readonly Error ExportGenerationFailed = Error.Failure("Course.ExportGenerationFailed", "Failed to generate export file.");

    // Permission errors
    public static readonly Error UnauthorizedAccess = Error.Unauthorized("Course.UnauthorizedAccess", "Unauthorized access to course.");
    public static readonly Error InsufficientPermissions = Error.Forbidden("Course.InsufficientPermissions", "Insufficient permissions to perform this action.");

    // Database errors
    public static readonly Error DatabaseError = Error.Failure("Course.DatabaseError", "Database operation failed.");
    public static readonly Error ConstraintViolation = Error.Conflict("Course.ConstraintViolation", "Database constraint violation occurred.");

    // File handling errors
    public static readonly Error FileUploadFailed = Error.Failure("Course.FileUploadFailed", "Failed to upload file.");
    public static readonly Error FileNotFound = Error.NotFound("Course.FileNotFound", "File not found.");
    public static readonly Error FileDeletionFailed = Error.Failure("Course.FileDeletionFailed", "Failed to delete file.");
    public static readonly Error InvalidFileFormat = Error.Validation("Course.InvalidFileFormat", "Invalid file format.");
    public static readonly Error FileTooLarge = Error.Validation("Course.FileTooLarge", "File size exceeds maximum allowed size.");

    // General errors
    public static readonly Error InvalidInput = Error.Validation("Course.InvalidInput", "Invalid input provided.");
    public static readonly Error OperationFailed = Error.Failure("Course.OperationFailed", "Operation failed.");
    public static readonly Error ServiceUnavailable = Error.Failure("Course.ServiceUnavailable", "Service is temporarily unavailable.");
} 