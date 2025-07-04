using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Syllabus.Domain.Services.File;
using Syllabus.Util.Options;
using System.Text.RegularExpressions;

namespace Syllabus.Infrastructure.Services.File
{
    /// <summary>
    /// Local file service implementation for handling file uploads.
    /// </summary>
    public class LocalFileService : IFileService
    {
        private readonly FileStorageOptions _fileStorageOptions;
        private readonly ILogger<LocalFileService> _logger;

        public LocalFileService(IOptions<FileStorageOptions> fileStorageOptions, ILogger<LocalFileService> logger)
        {
            _fileStorageOptions = fileStorageOptions.Value ?? throw new ArgumentNullException(nameof(fileStorageOptions));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> UploadProfilePictureAsync(string fileBase64, string fileName, string contentType, string userId)
        {
            try
            {
                // Validate file type
                if (!IsValidImageType(contentType))
                {
                    throw new ArgumentException("Invalid file type. Only JPEG, PNG, and GIF images are allowed.");
                }

                // Validate file size (max 5MB)
                var fileBytes = Convert.FromBase64String(fileBase64);
                if (fileBytes.Length > _fileStorageOptions.MaxFileSize)
                {
                    throw new ArgumentException($"File size too large. Maximum size is {_fileStorageOptions.MaxFileSize / (1024 * 1024)}MB.");
                }

                // Create directory if it doesn't exist
                var uploadPath = Path.Combine(_fileStorageOptions.UploadPath, "profile-pictures", userId);
                Directory.CreateDirectory(uploadPath);

                // Generate unique filename
                var fileExtension = Path.GetExtension(fileName);
                var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(uploadPath, uniqueFileName);

                // Save file
                await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);

                // Return the URL
                var fileUrl = $"{_fileStorageOptions.BaseUrl}/uploads/profile-pictures/{userId}/{uniqueFileName}";
                
                _logger.LogInformation("Profile picture uploaded successfully for user {UserId}. File: {FileName}", userId, uniqueFileName);
                
                return fileUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading profile picture for user {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> DeleteProfilePictureAsync(string fileUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(fileUrl))
                    return true;

                // Extract file path from URL
                var uri = new Uri(fileUrl);
                var relativePath = uri.AbsolutePath;
                
                // Remove the base path
                if (relativePath.StartsWith("/uploads/"))
                {
                    relativePath = relativePath.Substring("/uploads/".Length);
                }

                var fullPath = Path.Combine(_fileStorageOptions.UploadPath, relativePath);
                
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                    _logger.LogInformation("Profile picture deleted successfully: {FilePath}", fullPath);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting profile picture: {FileUrl}", fileUrl);
                return false;
            }
        }

        private bool IsValidImageType(string contentType)
        {
            var validTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
            return validTypes.Contains(contentType.ToLower());
        }
    }
} 