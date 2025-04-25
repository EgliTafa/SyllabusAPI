using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Syllabus.ApiContracts.Authentication;
using Syllabus.Application.Authentication.Login;
using Syllabus.Application.Authentication.Register;
using SyllabusAPI.Helpers;

namespace SyllabusAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestApiDTO request)
        {
            var command = new RegisterUserCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult(this);
        }

        /// <summary>
        /// Login with email and password.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestApiDTO request)
        {
            var command = new LoginUserCommand(request);
            var result = await _mediator.Send(command);
            return result.ToActionResult(this);
        }
    }
}
