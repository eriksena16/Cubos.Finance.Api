using Cubos.Finance.Application;
using Microsoft.AspNetCore.Authorization;

namespace Cubos.Finance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/accounts")]
    public class AccountsController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBankAccountService _accountService;

        public AccountsController(INotifier notifier, IUserService userService, IBankAccountService accountService): base(notifier) 
        {
            _userService = userService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var peopleId = _userService.GetPeopleId(User);
            var accounts = await _accountService.GetAccountsAsync(peopleId);

            return Ok(accounts);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] BankAccountRequest request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var peopleId = _userService.GetPeopleId(User);
            var account = await _accountService.CreateAsync(peopleId, request);

            return CustomResponse(account);
        }
        [HttpPost("{accountId}/cards")]
        public async Task<IActionResult> CreateCard(Guid accountId, [FromBody] CardRequest request)
        {
            //if (!ModelState.IsValid) return CustomResponse(ModelState);

            //var peopleId = _userService.GetPeopleId(User);
            //var account = await _accountService.CreateAsync(peopleId, request);

            return CustomResponse();
        }
    }

}
