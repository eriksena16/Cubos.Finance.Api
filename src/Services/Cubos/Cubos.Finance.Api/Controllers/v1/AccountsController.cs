using Cubos.Finance.Application;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Cubos.Finance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/accounts")]
    public class AccountsController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBankAccountService _accountService;

        public AccountsController(INotifier notifier, IUserService userService, IBankAccountService accountService) : base(notifier)
        {
            _userService = userService;
            _accountService = accountService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<BankAccountResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAccounts()
        {

            try
            {
                var peopleId = _userService.GetPeopleId(User);
                var accounts = await _accountService.GetAccountsAsync(peopleId);

                return CustomResponse(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno ao processar requicao.");
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(List<BankAccountResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateAccount([FromBody] BankAccountRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            try
            {

                var peopleId = _userService.GetPeopleId(User);
                var account = await _accountService.CreateAsync(peopleId, request);

                return CustomResponse(account);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno ao processar requicao.");
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);
        }
    }

}
