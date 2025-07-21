using CodePulse.Application.Auth;
using CodePulse.Application.Auth.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthAppService _authAppService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthAppService authAppService, ILogger<AuthController> logger)
        {
            _authAppService = authAppService;
            _logger = logger;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                var result = await _authAppService.RegisterAsync(request);

                if (result.Success)
                {
                    return Ok();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return ValidationProblem(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user");
                throw;
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var result = await _authAppService.LoginAsync(request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Invalid login attempt for {Email}", request.Email);
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in user");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
