using Cubos.Finance.Application;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Cubos.Finance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    public class CardsController(INotifier notifier, IUserService userService, ICardService cardService) : ApiControllerBase(notifier)
    {
        private readonly IUserService _userService = userService;
        private readonly ICardService _accountService = cardService;

        [HttpGet("accounts/{accountId}/cards")]
        [ProducesResponseType(typeof(List<CardResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetFromAccountCards([FromRoute] Guid accountId)
        {
            if (accountId == Guid.Empty)
                return CustomResponse(ModelState);

            try
            {
                var cards = await _accountService.GetCardsAsync(bankAccountId: accountId);

                return CustomResponse(cards);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno ao processar requicao.");
            }



        }
        [HttpGet("cards")]
        [ProducesResponseType(typeof(QueryBaseResponse<CardResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetFromPeopleCards([FromQuery] CardPaginationRequest cardFilter)
        {
            try
            {
                var peopleId = _userService.GetPeopleId(User);

                var cards = await _accountService.GetCardsFromPeopleAsync(peopleId, cardFilter);

                return CustomResponse(cards);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno ao processar requicao.");
            }
        }

        [HttpPost("accounts/{accountId}/cards")]
        [ProducesResponseType(typeof(CardResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateCard(Guid accountId, [FromBody] CardRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            try
            {

                var card = await _accountService.CreateCardAsync(accountId, request);

                return CustomResponse(card);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro interno ao processar requicao.");
            }
        }
    }

}
