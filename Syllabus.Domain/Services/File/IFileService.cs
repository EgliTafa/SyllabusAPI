namespace Syllabus.Domain.Services.File
{
    /// <summary>
    /// Interface for file service operations.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Uploads a profile picture and returns the URL.
        /// </summary>
        /// <param name="fileBase64">The file as a base64 string.</param>
        /// <param name="fileName">The original file name.</param>
        /// <param name="contentType">The content type of the file.</param>
        /// <param name="userId">The user ID for organizing files.</param>
        /// <returns>The URL of the uploaded file.</returns>
        Task<string> UploadProfilePictureAsync(string fileBase64, string fileName, string contentType, string userId);

        /// <summary>
        /// Deletes a profile picture.
        /// </summary>
        /// <param name="fileUrl">The URL of the file to delete.</param>
        /// <returns>True if deletion was successful.</returns>
        Task<bool> DeleteProfilePictureAsync(string fileUrl);
    }
} 