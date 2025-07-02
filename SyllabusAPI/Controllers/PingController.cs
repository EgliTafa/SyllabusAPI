using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Ping;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Syllabus.Infrastructure.Data;
using System.Collections.Generic;

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
        /// Public ping endpoint that doesn't require authentication for debugging.
        /// </summary>
        /// <returns>A success response indicating the API is accessible.</returns>
        /// <response code="200">API is accessible.</response>
        [HttpGet("public")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult PublicPing()
        {
            var response = new PingResponseApiDTO
            {
                Message = "API is accessible",
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

        /// <summary>
        /// Debug endpoint to list all registered routes.
        /// </summary>
        /// <returns>A list of all registered routes.</returns>
        /// <response code="200">Routes listed successfully.</response>
        [HttpGet("routes")]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult GetRoutes()
        {
            var routes = new List<object>();
            
            // This is a simplified version - in a real implementation, you'd use IActionDescriptorCollectionProvider
            routes.Add(new { Controller = "Auth", Routes = new[] { 
                "POST /auth/register",
                "POST /auth/login", 
                "POST /auth/forgot-password",
                "POST /auth/reset-password",
                "POST /auth/resend-email-confirmation",
                "PUT /auth/profile",
                "POST /auth/change-password",
                "POST /auth/upload-profile-picture"
            }});
            
            routes.Add(new { Controller = "Admin", Routes = new[] {
                "GET /api/Admin/users",
                "GET /api/Admin/users/search",
                "POST /api/Admin/users",
                "PUT /api/Admin/users/{userId}",
                "POST /api/Admin/users/{userId}/revoke",
                "POST /api/Admin/users/{userId}/restore",
                "DELETE /api/Admin/users/{userId}",
                "POST /api/Admin/users/{userId}/upload-profile-picture"
            }});
            
            routes.Add(new { Controller = "Ping", Routes = new[] {
                "GET /ping",
                "GET /ping/public",
                "GET /ping/routes",
                "GET /ping/health"
            }});

            return Ok(new { 
                Message = "Registered routes",
                Timestamp = DateTime.UtcNow,
                Routes = routes
            });
        }
    }
} 