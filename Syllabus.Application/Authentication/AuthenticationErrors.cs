using ErrorOr;

namespace Syllabus.Application.Authentication;

public static partial class AuthenticationErrors
{
    // User not found errors
    public static readonly Error UserNotFound = Error.NotFound("Authentication.UserNotFound", "User not found.");
    public static readonly Error UserByEmailNotFound = Error.NotFound("Authentication.UserByEmailNotFound", "User with this email was not found.");
    public static readonly Error UserByIdNotFound = Error.NotFound("Authentication.UserByIdNotFound", "User with the given ID was not found.");

    // Email validation errors
    public static readonly Error EmailRequired = Error.Validation("Authentication.EmailRequired", "Email is required.");
    public static readonly Error InvalidEmailFormat = Error.Validation("Authentication.InvalidEmailFormat", "Invalid email format.");
    public static readonly Error EmailAlreadyExists = Error.Conflict("Authentication.EmailAlreadyExists", "User with this email already exists.");
    public static readonly Error EmailAlreadyTaken = Error.Conflict("Authentication.EmailAlreadyTaken", "Email is already taken.");

    // Phone number errors
    public static readonly Error PhoneNumberAlreadyExists = Error.Conflict("Authentication.PhoneNumberAlreadyExists", "Phone number is already registered to another user.");
    public static readonly Error PhoneNumberRequired = Error.Validation("Authentication.PhoneNumberRequired", "Phone number is required.");
    public static readonly Error InvalidPhoneNumberFormat = Error.Validation("Authentication.InvalidPhoneNumberFormat", "Invalid phone number format.");

    // Password errors
    public static readonly Error PasswordRequired = Error.Validation("Authentication.PasswordRequired", "Password is required.");
    public static readonly Error InvalidPassword = Error.Validation("Authentication.InvalidPassword", "Invalid password.");
    public static readonly Error PasswordTooShort = Error.Validation("Authentication.PasswordTooShort", "Password must be at least 8 characters long.");
    public static readonly Error CurrentPasswordIncorrect = Error.Validation("Authentication.CurrentPasswordIncorrect", "Current password is incorrect.");
    public static readonly Error PasswordsDoNotMatch = Error.Validation("Authentication.PasswordsDoNotMatch", "Passwords do not match.");
    public static readonly Error PasswordChangeFailed = Error.Failure("Authentication.PasswordChangeFailed", "Failed to change password.");

    // Role errors
    public static readonly Error InvalidRole = Error.Validation("Authentication.InvalidRole", "Invalid role specified.");
    public static readonly Error RoleAssignmentFailed = Error.Failure("Authentication.RoleAssignmentFailed", "Failed to assign role to user.");
    public static readonly Error RoleRemovalFailed = Error.Failure("Authentication.RoleRemovalFailed", "Failed to remove role from user.");

    // Token errors
    public static readonly Error InvalidToken = Error.Validation("Authentication.InvalidToken", "Invalid or expired token.");
    public static readonly Error TokenRequired = Error.Validation("Authentication.TokenRequired", "Token is required.");
    public static readonly Error TokenExpired = Error.Validation("Authentication.TokenExpired", "Token has expired.");

    // Email confirmation errors
    public static readonly Error EmailConfirmationFailed = Error.Failure("Authentication.EmailConfirmationFailed", "Failed to send email confirmation.");
    public static readonly Error EmailConfirmationTokenInvalid = Error.Validation("Authentication.EmailConfirmationTokenInvalid", "Invalid email confirmation token.");

    // User creation/update errors
    public static readonly Error UserCreationFailed = Error.Failure("Authentication.UserCreationFailed", "Failed to create user.");
    public static readonly Error UserUpdateFailed = Error.Failure("Authentication.UserUpdateFailed", "Failed to update user.");
    public static readonly Error UserDeletionFailed = Error.Failure("Authentication.UserDeletionFailed", "Failed to delete user.");

    // Profile picture errors
    public static readonly Error ProfilePictureUploadFailed = Error.Failure("Authentication.ProfilePictureUploadFailed", "Failed to upload profile picture.");
    public static readonly Error InvalidFileFormat = Error.Validation("Authentication.InvalidFileFormat", "Invalid file format.");
    public static readonly Error FileTooLarge = Error.Validation("Authentication.FileTooLarge", "File size exceeds maximum allowed size.");

    // Lockout errors
    public static readonly Error UserLocked = Error.Validation("Authentication.UserLocked", "User account is locked.");
    public static readonly Error LockoutFailed = Error.Failure("Authentication.LockoutFailed", "Failed to lock user account.");
    public static readonly Error UnlockFailed = Error.Failure("Authentication.UnlockFailed", "Failed to unlock user account.");

    // Database errors
    public static readonly Error DatabaseError = Error.Failure("Authentication.DatabaseError", "Database operation failed.");
    public static readonly Error ConstraintViolation = Error.Conflict("Authentication.ConstraintViolation", "Database constraint violation occurred.");

    // General validation errors
    public static readonly Error InvalidInput = Error.Validation("Authentication.InvalidInput", "Invalid input provided.");
    public static readonly Error MissingRequiredFields = Error.Validation("Authentication.MissingRequiredFields", "Required fields are missing.");
    public static readonly Error Unauthorized = Error.Unauthorized("Authentication.Unauthorized", "Unauthorized access.");
    public static readonly Error Forbidden = Error.Forbidden("Authentication.Forbidden", "Access forbidden.");

    // Service errors
    public static readonly Error EmailServiceError = Error.Failure("Authentication.EmailServiceError", "Email service error occurred.");
    public static readonly Error ExternalServiceError = Error.Failure("Authentication.ExternalServiceError", "External service error occurred.");
} 