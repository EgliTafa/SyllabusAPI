using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Ping;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Syllabus.Infrastructure.Data;

namespace SyllabusAPI.Controllers
{
    /// <summary>
    /// Handles ping/health check operations for the API.
    /// </summary>
    [ApiController]
    [Route("ping")]
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Administrator")]
    public class PingController : ControllerBase
    {
        private readonly SyllabusDbContext _context;

        public PingController(SyllabusDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Simple ping endpoint to check if the API is running.
        /// </summary>
        /// <returns>A success response with timestamp indicating the API is operational.</returns>
        /// <response code="200">API is running successfully.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User does not have administrator role.</response>
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
        /// <response code="403">User does not have administrator role.</response>
        [HttpGet("auth")]
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

        /// <summary>
        /// Comprehensive health check endpoint that tests various system components.
        /// </summary>
        /// <returns>A detailed health status response.</returns>
        /// <response code="200">System is healthy.</response>
        /// <response code="503">System has health issues.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User does not have administrator role.</response>
        [HttpGet("health")]
        public async Task<IActionResult> HealthCheck()
        {
            var healthStatus = new HealthCheckResponseApiDTO
            {
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                Status = "Healthy",
                Checks = new List<HealthCheckItemApiDTO>()
            };

            // Check database connectivity
            try
            {
                await _context.Database.CanConnectAsync();
                healthStatus.Checks.Add(new HealthCheckItemApiDTO
                {
                    Name = "Database",
                    Status = "Healthy",
                    ResponseTime = 0, // Could be measured if needed
                    Message = "Database connection successful"
                });
            }
            catch (Exception ex)
            {
                healthStatus.Checks.Add(new HealthCheckItemApiDTO
                {
                    Name = "Database",
                    Status = "Unhealthy",
                    ResponseTime = 0,
                    Message = $"Database connection failed: {ex.Message}"
                });
                healthStatus.Status = "Unhealthy";
            }

            // Check API responsiveness
            healthStatus.Checks.Add(new HealthCheckItemApiDTO
            {
                Name = "API",
                Status = "Healthy",
                ResponseTime = 0,
                Message = "API is responsive"
            });

            // Check memory usage (basic)
            var memoryInfo = GC.GetGCMemoryInfo();
            var totalMemory = GC.GetTotalMemory(false);
            var memoryUsageMB = totalMemory / (1024 * 1024);
            
            healthStatus.Checks.Add(new HealthCheckItemApiDTO
            {
                Name = "Memory",
                Status = memoryUsageMB < 500 ? "Healthy" : "Warning",
                ResponseTime = 0,
                Message = $"Memory usage: {memoryUsageMB} MB"
            });

            if (memoryUsageMB >= 500)
            {
                healthStatus.Status = "Warning";
            }

            // Check uptime
            var uptime = DateTime.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime();
            healthStatus.Checks.Add(new HealthCheckItemApiDTO
            {
                Name = "Uptime",
                Status = "Healthy",
                ResponseTime = 0,
                Message = $"Uptime: {uptime.Days}d {uptime.Hours}h {uptime.Minutes}m"
            });

            var statusCode = healthStatus.Status == "Healthy" ? 200 : 503;
            return StatusCode(statusCode, healthStatus);
        }
    }
} 