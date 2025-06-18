using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Ping;
using System;

namespace SyllabusAPI.Controllers
{
    /// <summary>
    /// Handles ping/health check operations for the API.
    /// </summary>
    [ApiController]
    [Route("ping")]
    public class PingController : ControllerBase
    {
        /// <summary>
        /// Simple ping endpoint to check if the API is running.
        /// </summary>
        /// <returns>A success response with timestamp indicating the API is operational.</returns>
        /// <response code="200">API is running successfully.</response>
        [HttpGet]
        public IActionResult Ping()
        {
            var response = new PingResponseApiDTO
            {
                Message = "API is running",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0"
            };

            return Ok(response);
        }

        /// <summary>
        /// Authenticated ping endpoint to check if the API is running and the user is authenticated.
        /// </summary>
        /// <returns>A success response with user information and timestamp.</returns>
        /// <response code="200">API is running and user is authenticated.</response>
        /// <response code="401">User is not authenticated.</response>
        [HttpGet("auth")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult AuthenticatedPing()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            var response = new AuthenticatedPingResponseApiDTO
            {
                Message = "API is running and user is authenticated",
                Timestamp = DateTime.UtcNow,
                UserId = userId,
                UserEmail = userEmail,
                Version = "1.0.0"
            };

            return Ok(response);
        }
    }
} 