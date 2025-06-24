using ErrorOr;

namespace Syllabus.Application.Syllabus;

public static partial class SyllabusErrors
{
    // Syllabus not found errors
    public static readonly Error SyllabusNotFound = Error.NotFound("Syllabus.NotFound", "Syllabus not found.");
    public static readonly Error SyllabusByIdNotFound = Error.NotFound("Syllabus.ByIdNotFound", "Syllabus with the given ID was not found.");

    // Syllabus creation errors
    public static readonly Error SyllabusCreationFailed = Error.Failure("Syllabus.CreationFailed", "Failed to create syllabus.");
    public static readonly Error SyllabusAlreadyExists = Error.Conflict("Syllabus.AlreadyExists", "Syllabus with this name already exists.");
    public static readonly Error InvalidSyllabusData = Error.Validation("Syllabus.InvalidData", "Invalid syllabus data provided.");

    // Syllabus update errors
    public static readonly Error SyllabusUpdateFailed = Error.Failure("Syllabus.UpdateFailed", "Failed to update syllabus.");
    public static readonly Error SyllabusUpdateNoChanges = Error.Validation("Syllabus.UpdateNoChanges", "No changes were made to the syllabus.");

    // Syllabus deletion errors
    public static readonly Error SyllabusDeletionFailed = Error.Failure("Syllabus.DeletionFailed", "Failed to delete syllabus.");
    public static readonly Error SyllabusHasCourses = Error.Conflict("Syllabus.HasCourses", "Cannot delete syllabus that contains courses.");

    // Course management errors
    public static readonly Error CourseNotFound = Error.NotFound("Syllabus.CourseNotFound", "Course not found in syllabus.");
    public static readonly Error CourseAlreadyInSyllabus = Error.Conflict("Syllabus.CourseAlreadyInSyllabus", "Course is already in the syllabus.");
    public static readonly Error CourseNotInSyllabus = Error.Validation("Syllabus.CourseNotInSyllabus", "Course is not in the syllabus.");
    public static readonly Error CourseAdditionFailed = Error.Failure("Syllabus.CourseAdditionFailed", "Failed to add course to syllabus.");
    public static readonly Error CourseRemovalFailed = Error.Failure("Syllabus.CourseRemovalFailed", "Failed to remove course from syllabus.");

    // Program errors
    public static readonly Error ProgramNotFound = Error.NotFound("Syllabus.ProgramNotFound", "Program not found.");

    // Validation errors
    public static readonly Error InvalidSyllabusName = Error.Validation("Syllabus.InvalidName", "Invalid syllabus name.");
    public static readonly Error InvalidSyllabusDescription = Error.Validation("Syllabus.InvalidDescription", "Invalid syllabus description.");
    public static readonly Error InvalidSyllabusYear = Error.Validation("Syllabus.InvalidYear", "Invalid syllabus year.");
    public static readonly Error InvalidSyllabusSemester = Error.Validation("Syllabus.InvalidSemester", "Invalid syllabus semester.");
    public static readonly Error MissingRequiredFields = Error.Validation("Syllabus.MissingRequiredFields", "Required syllabus fields are missing.");

    // Export errors
    public static readonly Error ExportFailed = Error.Failure("Syllabus.ExportFailed", "Failed to export syllabus.");
    public static readonly Error ExportFormatNotSupported = Error.Validation("Syllabus.ExportFormatNotSupported", "Export format is not supported.");
    public static readonly Error ExportGenerationFailed = Error.Failure("Syllabus.ExportGenerationFailed", "Failed to generate export file.");

    // Permission errors
    public static readonly Error UnauthorizedAccess = Error.Unauthorized("Syllabus.UnauthorizedAccess", "Unauthorized access to syllabus.");
    public static readonly Error InsufficientPermissions = Error.Forbidden("Syllabus.InsufficientPermissions", "Insufficient permissions to perform this action.");

    // Database errors
    public static readonly Error DatabaseError = Error.Failure("Syllabus.DatabaseError", "Database operation failed.");
    public static readonly Error ConstraintViolation = Error.Conflict("Syllabus.ConstraintViolation", "Database constraint violation occurred.");

    // General errors
    public static readonly Error InvalidInput = Error.Validation("Syllabus.InvalidInput", "Invalid input provided.");
    public static readonly Error OperationFailed = Error.Failure("Syllabus.OperationFailed", "Operation failed.");
    public static readonly Error ServiceUnavailable = Error.Failure("Syllabus.ServiceUnavailable", "Service is temporarily unavailable.");
} 