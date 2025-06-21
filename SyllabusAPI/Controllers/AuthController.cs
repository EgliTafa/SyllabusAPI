using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Application.Authentication.ForgetAndReset;
using Syllabus.Application.Authentication.Login;
using Syllabus.Application.Authentication.Register;
using Syllabus.Application.Authentication.UpdateDetails;
using Syllabus.Application.Authentication.ChangePassword;
using Syllabus.Application.Authentication.ResendEmailConfirmation;
using SyllabusAPI.Helpers;
using System.Security.Claims;

namespace SyllabusAPI.Controllers
{
    /// <summary>
    /// Handles authentication-related operations such as user registration and login.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR sender for dispatching commands.</param>
        public AuthController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Registers a new user into the system.
        /// </summary>
        /// <param name="request">The registration request containing the user's email, password, and other required information.</param>
        /// <returns>A success result if registration completes; otherwise, returns error details.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Invalid request data or user already exists.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestApiDTO request)
        {
            var command = new RegisterUserCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Authenticates a user using email and password credentials.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>The JWT token and user details if authentication is successful; otherwise, returns error details.</returns>
        /// <response code="200">Login successful, JWT token provided.</response>
        /// <response code="401">Invalid email or password.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestApiDTO request)
        {
            var command = new LoginUserCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Sends a password reset email to the user if the email is registered.
        /// </summary>
        /// <param name="request">The forgot password request containing the user's email.</param>
        /// <returns>A success message if the process completes; otherwise, returns validation error details.</returns>
        /// <response code="200">Password reset email sent if user exists.</response>
        /// <response code="400">Invalid email address or format.</response>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordApiDTO request)
        {
            var command = new ForgotPasswordCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Resets the user's password using the token provided in the reset email.
        /// </summary>
        /// <param name="request">The reset password request containing the user's email, reset token, and new password.</param>
        /// <returns>A success message if password reset is successful; otherwise, returns validation errors.</returns>
        /// <response code="200">Password reset successful.</response>
        /// <response code="400">Invalid email, token, or password.</response>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordApiDTO request)
        {
            var command = new ResetPasswordCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Updates the profile details of the currently authenticated user.
        /// </summary>
        /// <param name="dto">The update request containing new profile information such as first name, last name, or email.</param>
        /// <returns>The updated user profile information if successful; otherwise, returns validation errors.</returns>
        /// <response code="200">Profile updated successfully.</response>
        /// <response code="400">Invalid request data or update failed due to validation errors.</response>
        /// <response code="401">User is not authenticated.</response>
        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDetailsApiDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new UpdateUserDetailsCommand(userId, dto));
            return result.ToActionResult(this);
        }


        /// <summary>
        /// Changes the password for the currently authenticated user.
        /// </summary>
        /// <param name="request">The change password request containing current and new password information.</param>
        /// <returns>A success result if password change completes; otherwise, returns error details.</returns>
        /// <response code="200">Password changed successfully.</response>
        /// <response code="400">Invalid request data or current password is incorrect.</response>
        /// <response code="401">User is not authenticated.</response>
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestApiDTO request)
        {
            var result = await _mediator.Send(new ChangePasswordCommand(request));
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Resends email confirmation to the user if the email is registered and not confirmed.
        /// </summary>
        /// <param name="request">The resend email confirmation request containing the user's email.</param>
        /// <returns>A success message if the process completes; otherwise, returns validation error details.</returns>
        /// <response code="200">Email confirmation sent if user exists and email is not confirmed.</response>
        /// <response code="400">Invalid email address or format.</response>
        [HttpPost("resend-email-confirmation")]
        public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendEmailConfirmationApiDTO request)
        {
            var command = new ResendEmailConfirmationCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult(this);
        }
    }
}
