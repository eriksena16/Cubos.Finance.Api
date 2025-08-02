using Cubos.Finance.Application;

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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PeopleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _peopleService.CreateAsync(request);

            return response != null
                ? Ok(response)
                : BadRequest();
        }

    }
}
