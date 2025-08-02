

using Cubos.Finance.Application;

namespace Cubos.Finance.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(INotifier notifier, IAuthService authService) : base(notifier)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.AuthenticateAsync(request);

            return response != null
                ? Ok(response)
                : BadRequest();
        }

    }
}
