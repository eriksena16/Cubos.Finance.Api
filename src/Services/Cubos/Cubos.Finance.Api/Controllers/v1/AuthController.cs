

using Cubos.Finance.Application;
using System.Net;

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
        [ProducesResponseType(typeof(BearerToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            try
            {
                var response = await _authService.AuthenticateAsync(request);


                return Ok(response); // 200
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno ao processar o login.");
            }
        }


    }
}
