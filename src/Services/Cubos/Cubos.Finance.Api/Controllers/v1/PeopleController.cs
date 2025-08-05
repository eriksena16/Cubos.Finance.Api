using Cubos.Finance.Application;
using System.Net;

namespace Cubos.Finance.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/people")]
    public class PeopleController : ApiControllerBase
    {
        private readonly IPeopleService _peopleService;
        public PeopleController(INotifier notifier, IPeopleService peopleService) : base(notifier)
        {
            _peopleService = peopleService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PeopleResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PeopleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));
            try
            {
                var response = await _peopleService.CreateAsync(request);

               return CustomResponse(response);
            }
            catch (Exception)
            {

                throw;
            }

          
        }

    }
}
