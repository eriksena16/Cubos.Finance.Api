using Cubos.Finance.Application;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Cubos.Finance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/accounts/{accountId}/transactions")]
    public class TransactionsController(INotifier notifier, IUserService userService, ITransactionService transactionService) : ApiControllerBase(notifier)
    {
        private readonly IUserService _userService = userService;
        private readonly ITransactionService _transactionService = transactionService;

        [HttpGet]
        [ProducesResponseType(typeof(QueryBaseResponse<TransactionResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetFromAccountTransactions(Guid accountId, [FromQuery] TransactionPaginationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            try
            {

                var transaction = await _transactionService.GetTransactionsAsync(accountId, request);

                return CustomResponse(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno ao processar requicao.");
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(TransactionResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterTransactionAsync(Guid accountId, [FromBody] TransactionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));
            try
            {

                var transaction = await _transactionService.RegisterTransactionAsync(accountId, request);

                return CustomResponse(transaction);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("internal")]
        [ProducesResponseType(typeof(TransactionInternalResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterTransactionInternalAsync(Guid accountId, [FromBody] TransactionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            try
            {

                var transaction = await _transactionService.RegisterTransactionInternalAsync(accountId, request);

                return CustomResponse(transaction);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
