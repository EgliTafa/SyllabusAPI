using MediatR;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Application.Authentication.Login;
using Syllabus.Application.Authentication.Register;
using SyllabusAPI.Helpers;

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
        /// <param name="request">The registration request containing the user’s email, password, and other required information.</param>
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
    }
}
